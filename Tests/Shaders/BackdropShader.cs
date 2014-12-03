using IIS.SLSharp.Annotations;
using IIS.SLSharp.Shaders;

namespace IIS.SLSharp.Examples.Complex.Shaders
{
	public abstract class BackdropShader : Shader
	{
		[Uniform]
		public abstract float MaxScale { get; set; }

		[Uniform]
		public abstract vec2 Scale { get; set; }

		[Varying]
		private vec2 _uv;

		[Varying ( UsageSemantic.Position0 )]
		private vec4 _position;

		[FragmentOut ( UsageSemantic.Color0 )]
		protected vec4 Color;

		[VertexIn ( UsageSemantic.Position0 )]
		public vec4 GlVertex;

		// demonstrate using a shared library
		public WangShader Wang { get; private set; }

		[FragmentShader ( true )]
		protected void FragmentMain ()
		{
			Color = Wang.WangAt ( _uv );
		}

		[VertexShader ( true )]
		public void VertexMain ()
		{
			_uv = GlVertex.xy * 0.5f * Scale * MaxScale;
			_position = GlVertex;
		}

		// resource setup code
		protected BackdropShader ()
		{
			Wang = CreateSharedShader<WangShader> ();
			Link ( new[] { Wang } );
		}
	}
}