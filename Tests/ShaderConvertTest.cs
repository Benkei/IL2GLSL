using System.Collections.Generic;
using System.Reflection;
using IIS.SLSharp;
using IIS.SLSharp.Bindings;
using IIS.SLSharp.Descriptions;
using IIS.SLSharp.Examples.Complex.Shaders;
using IIS.SLSharp.Reflection;
using IIS.SLSharp.Shaders;
using IIS.SLSharp.Translation;
using IIS.SLSharp.Translation.GLSL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
	[TestClass]
	public class ShaderConvertTest
	{
		[TestMethod]
		public void TestMethod1 ()
		{
			Binding.Register ( new Test () );

			var _backdropShader = Shader.CreateSharedShader<BackdropShader> ();

			var _cubeShader = Shader.CreateSharedShader<CubeShader> ();
		}
	}


	public class Test : ISLSharpBinding
	{
		public Dictionary<ReflectionToken, MethodInfo> PassiveMethods
		{
			get { return new Dictionary<ReflectionToken, MethodInfo> (); }
		}

		public ITransform Transform
		{
			get { return null; }
		}

		public void TexActivate ( int textureUnit, object tex )
		{
		}

		public void TexFinish ( int textureUnit, object tex )
		{
		}

		public void TexReset ()
		{
		}

		public object Compile ( Shader shader, ShaderType typ, SourceDescription source )
		{
			var src = source.ToGlsl ( typ );
			//throw new System.NotImplementedException ();
			return null;
		}

		public IProgram Link ( Shader shader, IEnumerable<object> units )
		{
			return null;
		}

		public void Initialize ()
		{
		}

		public void Cleanup ()
		{
		}

		public void FullscreenQuad ( int vertexLocation, bool positive )
		{
		}
	}
}