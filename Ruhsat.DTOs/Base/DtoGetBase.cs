using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DTOs.Base
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
}
