namespace LateralTransitionsNET.Views;

public class SegmentedButtonsView : UIView
{
    private UIView selectorView = new UIView();
    private List<UILabel> labels = new List<UILabel>();
    private string[] titles;
    private UIColor textColor = UIColor.LightGray;
    private UIColor selectorTextColor = UIColor.Black;
    public int selectedIndex { get; private set; } = 0;
    
    public event EventHandler<int> DidIndexChanged;
    
    private void ConfigSelectedTap()
    {
        float segmentsCount = titles.Length;
        float selectorWidth = (float)Frame.Width / segmentsCount;
        selectorView = new UIView(new CoreGraphics.CGRect(0, Frame.Height - 0.8, selectorWidth, 0.5))
        {
            BackgroundColor = UIColor.Black
        };
        AddSubview(selectorView);
    }

    private void UpdateView()
    {
        CreateLabels();
        ConfigSelectedTap();
        ConfigStackView();
    }

    public override void Draw(CoreGraphics.CGRect rect)
    {
        base.Draw(rect);
        UpdateView();
    }

    private void CreateLabels()
    {
        labels.Clear();
        
        foreach (var uiView in Subviews)
        {
            uiView.RemoveFromSuperview();
        }
        
        foreach (var labelTitle in titles)
        {
            var label = new UILabel
            {
                Font = UIFont.SystemFontOfSize(18),
                Text = labelTitle,
                TextColor = textColor,
                TextAlignment = UITextAlignment.Center
            };
            var tapGestureRecognizor = new UITapGestureRecognizer(LabelActionHandler);
            tapGestureRecognizor.NumberOfTapsRequired = 1;
            label.AddGestureRecognizer(tapGestureRecognizor);
            label.UserInteractionEnabled = true;
            labels.Add(label);
        }
        labels[0].TextColor = selectorTextColor;
    }

    public void SetLabelsTitles(string[] titles)
    {
        this.titles = titles;
        UpdateView();
    }

    private void ConfigStackView()
    {
        var stackView = new UIStackView(labels.ToArray())
        {
            Axis = UILayoutConstraintAxis.Horizontal,
            Alignment = UIStackViewAlignment.Fill,
            Distribution = UIStackViewDistribution.FillEqually
        };
        AddSubview(stackView);
        stackView.TranslatesAutoresizingMaskIntoConstraints = false;
        stackView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
        stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
        stackView.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
        stackView.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
    }

    private void LabelActionHandler(UITapGestureRecognizer sender)
    {
        // foreach (var (labelIndex, lbl) in labels.Enumerate())
        foreach (var (lbl, labelIndex) in labels.Select((item, index) => (item, index)))
        {
            if (lbl == sender.View)
            {
                float selectorPosition = (float)Frame.Width / titles.Length * labelIndex;
                selectedIndex = labelIndex;
                DidIndexChanged?.Invoke(this, labelIndex);
                UIView.Animate(0.1, () =>
                {
                    selectorView.Frame = new CoreGraphics.CGRect(selectorPosition, selectorView.Frame.Y, selectorView.Frame.Width, selectorView.Frame.Height);
                });
            }
        }
    }

    public void CollectionViewDidScroll(float x)
    {
        UIView.Animate(0.1, () =>
        {
            selectorView.Frame = new CoreGraphics.CGRect(x, selectorView.Frame.Y, selectorView.Frame.Width, selectorView.Frame.Height);

            foreach (var view in Subviews)
            {
                if (view is UIStackView stack)
                {
                    foreach (var subview in stack.ArrangedSubviews)
                    {
                        if (subview is UILabel label)
                        {
                            if ((label.Frame.Width / 2 >= selectorView.Frame.X && titles[0] == label.Text) || (label.Frame.Width / 2 <= selectorView.Frame.X && titles[1] == label.Text))
                            {
                                label.TextColor = selectorTextColor;
                            }
                            else
                            {
                                label.TextColor = textColor;
                            }
                        }
                    }
                }
            }
        });
    }
}