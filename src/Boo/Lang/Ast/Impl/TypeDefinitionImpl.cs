#region license
// boo - an extensible programming language for the CLI
// Copyright (C) 2004 Rodrigo B. de Oliveira
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// As a special exception, if you link this library with other files to
// produce an executable, this library does not by itself cause the
// resulting executable to be covered by the GNU General Public License.
// This exception does not however invalidate any other reasons why the
// executable file might be covered by the GNU General Public License.
//
// Contact Information
//
// mailto:rbo@acm.org
#endregion

//
// DO NOT EDIT THIS FILE!
//
// This file was generated automatically by the
// ast.py script on Mon Feb 02 15:44:49 2004
//
using System;

namespace Boo.Lang.Ast.Impl
{
	[Serializable]
	public abstract class TypeDefinitionImpl : TypeMember
	{
		protected TypeMemberCollection _members;
		protected TypeReferenceCollection _baseTypes;
		
		protected TypeDefinitionImpl()
		{
			_members = new TypeMemberCollection(this);
			_baseTypes = new TypeReferenceCollection(this);
 		}
		
		protected TypeDefinitionImpl(LexicalInfo lexicalInfo) : base(lexicalInfo)
		{
			_members = new TypeMemberCollection(this);
			_baseTypes = new TypeReferenceCollection(this);
 		}
		
		public TypeMemberCollection Members
		{
			get
			{
				return _members;
			}
			
			set
			{
				
				if (_members != value)
				{
					_members = value;
					if (null != _members)
					{
						_members.InitializeParent(this);
					}
				}
			}
		}
		public TypeReferenceCollection BaseTypes
		{
			get
			{
				return _baseTypes;
			}
			
			set
			{
				
				if (_baseTypes != value)
				{
					_baseTypes = value;
					if (null != _baseTypes)
					{
						_baseTypes.InitializeParent(this);
					}
				}
			}
		}
	}
}
