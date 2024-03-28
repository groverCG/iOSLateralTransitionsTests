namespace LateralTransitionsNET.Cells;

public class UserTableViewCell : UITableViewCell
{
    public static NSString ReuseIdentifier = new NSString("UserTableViewCell");
    
    bool followers = false;
    public bool Followers
    {
        get => followers;
        set
        {
            followers = value;
            if (!followers)
            {
                FollowButton.SetTitle("Following", UIControlState.Normal);
            }
            else
            {
                FollowButton.SetTitle("Remove", UIControlState.Normal);
            }
        }
    }
    
    public UIImageView ProfileImageView = new UIImageView
    {
        ContentMode = UIViewContentMode.ScaleAspectFill,
        BackgroundColor = UIColor.LightGray,
        ClipsToBounds = true,
        TranslatesAutoresizingMaskIntoConstraints = false
    };
    
    public UIButton FollowButton = new UIButton(UIButtonType.System)
    {
        TranslatesAutoresizingMaskIntoConstraints = false,
        BackgroundColor = UIColor.White,
        Layer = { BorderWidth = 0.5f, BorderColor = UIColor.Black.CGColor, CornerRadius = 3 },
        ContentEdgeInsets = new UIEdgeInsets(5, 5, 5, 5)
    };
    
    public UILabel UserNameLabel = new UILabel
    {
        TranslatesAutoresizingMaskIntoConstraints = false,
        Font = UIFont.BoldSystemFontOfSize(15),
        Text = "Username"
    };
    
    public UserTableViewCell(IntPtr handle) : base(handle)
    {
        ConfigUI();
    }
    
    public override void AwakeFromNib()
    {
        base.AwakeFromNib();
        FollowButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
    }
    
    void ConfigUI()
    {
        AddSubview(ProfileImageView);
        
        ProfileImageView.LeftAnchor.ConstraintEqualTo(LeftAnchor, 20).Active = true;
        ProfileImageView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
        ProfileImageView.HeightAnchor.ConstraintEqualTo(48).Active = true;
        ProfileImageView.WidthAnchor.ConstraintEqualTo(48).Active = true;
        
        ProfileImageView.Layer.CornerRadius = 48 / 2;
        
        AddSubview(UserNameLabel);
        
        UserNameLabel.LeftAnchor.ConstraintEqualTo(ProfileImageView.RightAnchor, 15).Active = true;
        UserNameLabel.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
        
        AddSubview(FollowButton);
        FollowButton.RightAnchor.ConstraintEqualTo(RightAnchor, -10).Active = true;
        FollowButton.WidthAnchor.ConstraintEqualTo(75).Active = true;
        FollowButton.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
    }
}
