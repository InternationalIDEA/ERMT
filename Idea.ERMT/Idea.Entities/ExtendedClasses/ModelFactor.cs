﻿using System;

namespace Idea.Entities
{
    public partial class ModelFactor
    {
        partial void OnCreated()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
         }
    }
}
