﻿#region license
// Copyright (c) 2003, 2004, 2005 Rodrigo B. de Oliveira (rbo@acm.org)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Rodrigo B. de Oliveira nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Runtime.CompilerServices;

namespace Boo.Lang.Environments
{
	/// <summary>
	/// Idiomatic access to environmental services.
	/// 
	/// <example>
	/// <code>
	///	<![CDATA[
	/// if (My<TypeSystemServices>.Instance.IsPrimitive(someType))
	///		return true;
	/// ]]>
	/// </code>
	/// </example>
	/// </summary>
	/// <typeparam name="TNeed">The type of the requested service.</typeparam>
	public static class My<TNeed> where TNeed : class
	{
		public static TNeed Instance
		{
			get
			{
                var environment = Environment.CurrentEnvironment;
				if (null == environment)
					throw new InvalidOperationException("Environment is not available!");
			    return environment.Provide<TNeed>();
			}
		}
	}

    public interface IEnvironment
    {
        TNeed Provide<TNeed>() where TNeed : class;
    }

    public class ClosedEnvironment : IEnvironment
    {
        private readonly object[] _bindings;

        public ClosedEnvironment(params object[] bindings)
        {
            _bindings = bindings;
        }

        public TNeed Provide<TNeed>() where TNeed : class
        {
            foreach (var binding in _bindings)
                if (binding is TNeed)
                    return (TNeed)binding;
            return null;
        }
    }

    [CompilerGlobalScope]
    public static class Environment
    {
        public static IEnvironment CurrentEnvironment { get { return _currentEnvironment; } }

        public static void With(IEnvironment environment, Action action)
        {
            var previous = _currentEnvironment;
            try
            {
                _currentEnvironment = environment;
                action(); 
            }
            finally
            {
                _currentEnvironment = previous;
            }
        }

        [ThreadStatic]
        private static IEnvironment _currentEnvironment;
    }
}
