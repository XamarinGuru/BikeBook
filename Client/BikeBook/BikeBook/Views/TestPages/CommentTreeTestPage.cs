using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using BikeBook.Views.CustomUIElements;

namespace BikeBook.Views.TestPages
{
    public class CommentTreeTestPage : ContentPage
    {
        NewsfeedPostCell m_newsFeedCell;
        CommentTreeFrame m_commentTree;

        public CommentTreeTestPage()
        {
            BackgroundColor = Color.FromHex(UIColors.COLOR_PAGE_BACKGROUND);
            Padding = UISizes.PADDING_NONE;

            m_newsFeedCell = new NewsfeedPostCell();

            m_commentTree = new CommentTreeFrame();
            m_commentTree.AddTalkingHead();
            m_commentTree.AddHeader();
            m_commentTree.AddChild();
            m_commentTree.ShowEntry();

            StackLayout ContentStack = new StackLayout()
            {
                Children =
                {
                    m_newsFeedCell,
                    m_commentTree,
                },
            };

            Content = new ScrollView
            {
                Padding = UISizes.PADDING_STANDARD,
                Content = ContentStack,
            };
        }
    }
}
