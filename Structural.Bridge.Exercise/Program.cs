using System;

namespace Structural.Bridge.Exercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        //public string WhatToRenderAs { get { return "lines"; } }
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        //public string WhatToRenderAs => throw new NotImplementedException();
        //public string WhatToRenderAs { get { return "pixels"; } }
        public string WhatToRenderAs => "pixels";
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        public string Name { get; set; }

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }
}