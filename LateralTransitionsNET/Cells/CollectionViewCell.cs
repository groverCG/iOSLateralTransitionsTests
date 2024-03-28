namespace LateralTransitionsNET.Cells;

public class CollectionViewCell : UICollectionViewCell
{
    public static String ReuseIdentifier = "CollectionViewCell";
    
    public string[] currentData;
    public string tabType;
    
    public UITableView tableView = new UITableView
    {
        TranslatesAutoresizingMaskIntoConstraints = false
    };
    
    public CollectionViewCell(IntPtr handle) : base(handle)
    {
        ConfigUI();
        BackgroundColor = UIColor.Red;
    }
    
    void ConfigUI()
    {
        AddSubview(tableView);
        tableView.RightAnchor.ConstraintEqualTo(RightAnchor).Active = true;
        tableView.LeftAnchor.ConstraintEqualTo(LeftAnchor).Active = true;
        tableView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
        tableView.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;

        tableView.RegisterClassForCellReuse(typeof(UserTableViewCell), UserTableViewCell.ReuseIdentifier);
        tableView.AllowsSelection = true;
        tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
    }
}

public class UserTableViewDataSource : UITableViewSource
{
    public string[] CurrentData;
    public string TabType;
    
    public UserTableViewDataSource(string[] currentData, string tabType)
    {
        CurrentData = currentData;
        TabType = tabType;
    }
    
    public override IntPtr RowsInSection(UITableView tableView, IntPtr section)
    {
        return CurrentData?.Length ?? 0;
    }

    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
    {
        return 60;
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
        var cell = (UserTableViewCell)tableView.DequeueReusableCell(UserTableViewCell.ReuseIdentifier, indexPath);
        
        if (!string.IsNullOrEmpty(TabType))
        {
            if (TabType == "followers")
            {
                cell.UserNameLabel.Text = CurrentData[indexPath.Row];
                cell.Followers = true;
            }
            else
            {
                cell.Followers = false;
                cell.UserNameLabel.Text = CurrentData[indexPath.Row];
            }
        }
        
        cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
    }
} 
