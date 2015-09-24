using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;

namespace PopupDictionairy.App.Controller
{
    public interface ITranslationQuestionSelector
    {
        IEnumerable<Translation> GetBatch(int take, IEnumerable<Translation> source);
    }
}
