using IIS.SLSharp.Annotations;
using IIS.SLSharp.Shaders;

namespace IIS.SLSharp.Examples.Complex.Shaders
{
	/// <summary>
	/// Implements a simplex noise shader as in
	/// http://http.developer.nvidia.com/GPUGems2/gpugems2_chapter26.html
	/// </summary>
	public abstract class SimplexNoiseShader : Shader
	{
		[Uniform]
		protected abstract sampler1D PermGradSampler { set; get; }

		[Uniform]
		protected abstract sampler2D PermSampler2D { set; get; }

		[FragmentShader]
		protected vec3 Fade ( vec3 t )
		{
			return t * t * t * (t * (t * 6.0f - 15.0f) + 10.0f); // new curve
		}

		[FragmentShader]
		protected vec4 Perm2D ( vec2 p )
		{
			return Texture ( PermSampler2D, p );
		}

		[FragmentShader]
		protected float GradPerm ( float x, vec3 p )
		{
			return Dot ( Texture ( PermGradSampler, x ).xyz, p );
		}

		[FragmentShader]
		protected float Noise ( vec3 p )
		{
			var q = Floor ( p ) % 256.0f;
			p -= Floor ( p );
			var f = Fade ( p );

			q = q / 256.0f;
			const float one = 1.0f / 256.0f;

			var aa = Perm2D ( q.xy ) + q.z;
			var zo = new vec2 ( 0.0f, 1.0f );

			return Lerp ( Lerp ( Lerp ( GradPerm ( aa.x, p ), GradPerm ( aa.z, p - zo.yxx ), f.x ),
				Lerp ( GradPerm ( aa.y, p - zo.xyx ), GradPerm ( aa.w, p - zo.yyx ), f.x ),
				f.y ),
				Lerp ( Lerp ( GradPerm ( aa.x + one, p - zo.xxy ), GradPerm ( aa.z + one, p - zo.yxy ), f.x ),
				Lerp ( GradPerm ( aa.y + one, p - zo.xyy ), GradPerm ( aa.w + one, p - zo.yyy ), f.x ),
				f.y ),
				f.z );
		}

		[FragmentShader]
		public float FBm ( vec3 p, int octaves, float lacunarity, float gain )
		{
			var freq = 1.0f;
			var amp = 1.0f;
			var sum = 0.0f;

			for ( var i = 0; i < octaves; i++ )
			{
				sum += Noise ( p * freq ) * amp;
				freq *= lacunarity;
				amp *= gain;
			}

			return sum;
		}
	}
}