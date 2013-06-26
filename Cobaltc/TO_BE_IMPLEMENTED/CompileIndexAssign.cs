using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public partial class CodeAnalyser
	{
		public void CompileGetIndex(GetAccessor Get)
		{
			VType t = GuessPtrType(Get.Var);
			if(t == VType.Unknown)
				Errors.Add(Get.Var + " does not exist in the current context!");
			else
			{
				if(t == VType.Int32)
				{
					CompileIntExpression(Get.Index);
					GetIntValue(Get.Var);
				}
				else if (t == VType.Int8)
				{
					CompileIntExpression(Get.Index);
					GetIntValue(Get.Var);
				}
			}
		}
		public void CompileSetIndex(SetAccessor Set)
		{
			VType t = GuessPtrType(Set.Var);
			if(t == VType.Unknown)
				Errors.Add(Set.Var + " does not exist in the current context!");
			else
			{
				if(t == VType.Int32)
				{
					CompileIntExpression(Set.Value);
					CompileIntExpression(Set.Index);
					GetIntValue(Set.Var);
				}
				else if (t == VType.Int8)
				{
					CompileCharExpression(Set.Value);
					CompileIntExpression(Set.Index);
					GetIntValue(Set.Var);
				}
				
				
			}
		}
	}
	
}