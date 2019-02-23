using System;
using UIKit;
using Foundation;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using XamAppCenterSample2018.ViewModels;

namespace XamAppCenterSample2018.iOS.Views
{
    [Register("MainView")]
    [MvxRootPresentation(WrapInNavigationController = false)]
    public class MainView : MvxViewController<MainViewModel>
    {
        static readonly nfloat fontSize = 20;

        UILabel inputLabel;
        UITextView inputText;
        UIButton translateButton;
        UILabel translatedLabel;
        UITextView translatedText;

        public MainView()
        {
        }

        /// <summary>
        /// 初期化処理（InitializeComponentに相当する）
        /// </summary>
        void InitUI() 
        {
            View.ContentMode = UIViewContentMode.ScaleToFill; // アスペクト比を保ったまま引き伸ばし
            View.LayoutMargins = new UIEdgeInsets(0, 16, 0, 16);
            View.Frame = new CGRect(0, 0, 375, 667); // 仮想解像度
            View.BackgroundColor = UIColor.White; 
            // 物理的な解像度と論理的な解像度が違う場合に設定すると良い
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            // 翻訳したい日本語ラベル
            inputLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 375, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "翻訳したい日本語",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(inputLabel);

            inputLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            inputLabel.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            inputLabel.TopAnchor.ConstraintEqualTo(View.TopAnchor, 70).Active = true;
            inputLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            inputLabel.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            // 翻訳したい日本語の入力欄
            inputText = new UITextView
            {
                Frame = new CGRect(0, 0, 375, 200),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                KeyboardType = UIKeyboardType.Twitter,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "inputText",
            };

            inputText.Layer.BorderWidth = 1;
            inputText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(inputText);

            inputText.HeightAnchor.ConstraintEqualTo(View.HeightAnchor, 0.3f).Active = true;
            inputText.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            inputText.TopAnchor.ConstraintEqualTo(inputLabel.BottomAnchor, 5).Active = true;
            inputText.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            inputText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            // 入力完了時にソフトキーボードを閉じるボタンの設定
            var toolBar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            toolBar.HeightAnchor.ConstraintEqualTo(40).Active = true;
            toolBar.WidthAnchor.ConstraintEqualTo(View.Frame.Width).Active = true;

            var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            var commitButton = new UIBarButtonItem(UIBarButtonSystemItem.Done);

            commitButton.Clicked += (s, e) => View.EndEditing(true);
            toolBar.SetItems(new UIBarButtonItem[] { spacer, commitButton }, false);
            inputText.InputAccessoryView = toolBar;

            // 英語に翻訳するボタン
            translateButton = new UIButton(UIButtonType.RoundedRect)
            {
                Frame = new CGRect(0, 0, 375, 20),
                Opaque = false,
                ContentMode = UIViewContentMode.ScaleToFill,
                HorizontalAlignment = UIControlContentHorizontalAlignment.Center,
                VerticalAlignment = UIControlContentVerticalAlignment.Center,
                LineBreakMode = UILineBreakMode.MiddleTruncation,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "translateButton",
            };

            translateButton.SetTitle("英語に翻訳する", UIControlState.Normal);
            View.AddSubview(translateButton);

            translateButton.HeightAnchor.ConstraintEqualTo(40f).Active = true;
            translateButton.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            translateButton.TopAnchor.ConstraintEqualTo(inputText.BottomAnchor, 20).Active = true;
            translateButton.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            translateButton.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            // 翻訳された英語のラベル
            translatedLabel = new UILabel
            {
                Frame = new CGRect(0, 0, 375, 20),
                Opaque = false,
                UserInteractionEnabled = false,
                ContentMode = UIViewContentMode.Left,
                Text = "翻訳された英語",
                TextAlignment = UITextAlignment.Left,
                LineBreakMode = UILineBreakMode.TailTruncation,
                Lines = 0,
                BaselineAdjustment = UIBaselineAdjustment.AlignBaselines,
                AdjustsFontSizeToFitWidth = false,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
            };
            View.AddSubview(translatedLabel);

            translatedLabel.HeightAnchor.ConstraintEqualTo(20).Active = true;
            translatedLabel.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            translatedLabel.TopAnchor.ConstraintEqualTo(translateButton.BottomAnchor, 20).Active = true;
            translatedLabel.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            translatedLabel.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;

            // 翻訳された英語の表示欄
            translatedText = new UITextView
            {
                Frame = new CGRect(0, 0, 375, 200),
                ContentMode = UIViewContentMode.ScaleToFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(fontSize),
                AccessibilityIdentifier = "translatedText",
                Editable = false,
            };

            translatedText.Layer.BorderWidth = 1;
            translatedText.Layer.BorderColor = UIColor.LightGray.CGColor;

            View.AddSubview(translatedText);

            translatedText.HeightAnchor.ConstraintEqualTo(View.HeightAnchor, 0.3f).Active = true;
            translatedText.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;

            translatedText.TopAnchor.ConstraintEqualTo(translatedLabel.BottomAnchor, 5).Active = true;
            translatedText.LeftAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.LeftAnchor).Active = true;
            translatedText.RightAnchor.ConstraintEqualTo(View.LayoutMarginsGuide.RightAnchor).Active = true;
        }

        /// <summary>
        /// バインディングを設定するメソッド
        /// </summary>
        void SetBinding()
        {
            var set = this.CreateBindingSet<MainView, MainViewModel>();

            set.Bind(inputText).To(vm => vm.InputText);
            set.Bind(translatedText).To(vm => vm.TranslatedText);
            set.Bind(translateButton).To(vm => vm.TranslateCommand);

            set.Apply();
        }

        /// <summary>
        /// 作成したメソッドの呼び出し
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();
            SetBinding();
        }
    }
}
