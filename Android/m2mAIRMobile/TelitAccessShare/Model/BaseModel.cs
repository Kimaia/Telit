using System;

namespace Shared.Model
{
    public abstract class BaseModel
    {

        public delegate void OnError(string title,string message);

        public delegate void OnSuccess();

        public BaseModel()
        {
        }
    }
}

