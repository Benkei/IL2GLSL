﻿using IIS.SLSharp.Annotations;
using IIS.SLSharp.Shaders;

namespace IIS.SLSharp.Examples.Complex.Shaders
{
	public abstract class WangShader : Shader
	{
		[Uniform]
		public abstract sampler2D WangTiles { set; get; }

		[Uniform]
		public abstract sampler2D WangMap { set; get; }

		[FragmentShader]
		public vec4 WangAt ( vec2 tex )
		{
			var address = tex - tex % (1.0f / 256.0f);
			var subPos = Fraction ( tex * 256.0f ) / 4.0f;
			var offset = Texture ( WangMap, Fraction ( address ) ).xw;
			var tc = offset + subPos;
			var tileScaledTex = tex * new vec2 ( 32.0f / 1.0f );

			return TextureGrad ( WangTiles, tc, DeriveTowardsX ( tileScaledTex ), DeriveTowardsY ( tileScaledTex ) );
		}
	}
}