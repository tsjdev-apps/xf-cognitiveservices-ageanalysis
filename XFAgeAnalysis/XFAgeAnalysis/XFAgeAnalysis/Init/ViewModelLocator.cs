using CommonServiceLocator;
using XFAgeAnalysis.ViewModels;

namespace XFAgeAnalysis.Init
{
    public class ViewModelLocator
    {
        public AgeAnalyzeViewModel AgeAnalyzeViewModel
            => ServiceLocator.Current.GetInstance<AgeAnalyzeViewModel>();
    }
}
