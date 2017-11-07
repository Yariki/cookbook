using System.Windows;
using System.Windows.Controls;
using Cookbook.Client.Module.Interfaces.MVVM;

namespace Cookbook.Client.Module.Core.TemplateSelectors
{
    public class BSWorkspaceSelector : DataTemplateSelector
    {

        public DataTemplate GridTemplate { get; set; }

        public DataTemplate RecipeTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if ((item is ContentPresenter) && (((ContentPresenter)item).Content is IBSDataViewModel))
            {
                return RecipeTemplate;
            }
            return GridTemplate;
        }
    }
}