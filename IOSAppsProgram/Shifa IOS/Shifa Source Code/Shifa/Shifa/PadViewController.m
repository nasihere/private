//
//  PadViewController.m
//  Shifa
//
//  Created by My Mac on 4/23/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "PadViewController.h"
#import "kentVC2Cell.h"
#import "SearchObjectClass.h"

@interface PadViewController ()

@end

@implementation PadViewController
@synthesize strPassStringKentVC2toPadVC;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
#pragma mark - Life Cycle

- (void)viewDidLoad
{
    
    [super viewDidLoad];
    app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
    progressView = [[MBProgressHUD alloc]initWithView:self.view];
    progressView.labelText = @"Please wait...";
    [self.view addSubview:progressView];
    [progressView show:YES];
    
    _tableViewKentPadVC.delegate =self;
    _tableViewKentPadVC.dataSource = self;
    
 if ([_tableViewKentPadVC respondsToSelector:@selector(setSeparatorInset:)])
    {
        [_tableViewKentPadVC setSeparatorInset:UIEdgeInsetsZero];
    }
    
  
    arrRowIdPadVc                    =[[NSMutableArray alloc]init];
    arrRowSublevelPadVc              =[[NSMutableArray alloc]init];
    arrRowTitlePadVc                 =[[NSMutableArray alloc]init];
    arrRowSubTitlePadVc              =[[NSMutableArray alloc]init];
    arrRowIntencityPadVc             =[[NSMutableArray alloc]init];
    arrRowRemediesPadVc              =[[NSMutableArray alloc]init];
    arrSearchResults                 =[[NSArray alloc]init];
    arrAllObjects                    =[[NSMutableArray alloc]init];

   
    //Create Dummy Tview To Calculate Height
    txt_ToCalculateHeight = [[UITextView alloc]initWithFrame:CGRectMake(0, 0, 320, 200)];
    [self.view addSubview:txt_ToCalculateHeight];
    txt_ToCalculateHeight.hidden = YES;

}

-(void)viewWillAppear:(BOOL)animated
{
    
    [super viewWillAppear:animated];
    [self performSelector:@selector(getPadVcDataWhereCategory:) withObject:app.strPassStringKentVC2toPadVC afterDelay:0.0];
    [self getCheckboxSelected_Ids];
    _tableViewKentPadVC.contentOffset = CGPointMake(0, self.searchDisplayController.searchBar.frame.size.height+2);


}
-(void)viewWillDisappear:(BOOL)animated
{
    [super viewWillDisappear:animated];
  

    
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark -Actions
-(void)getPadVcDataWhereCategory:(NSString *)category
{

        category = [category stringByAppendingString:@"%"];
    
        NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id,Name,categoy,Intensity,Remedies,newrem,sublevel FROM tbl_shifa where categoy LIKE '%@' COLLATE NOCASE order by newrem",category];
       DLog(@"Quary At PAD Screen==%@",QuaryString);

        FMResultSet *results = [app.database executeQuery:QuaryString];
        while([results next]) {
            
            SearchObjectClass *object = [SearchObjectClass new];
            
            NSString *tempId = [results stringForColumn:@"_id"];
            [arrRowIdPadVc addObject:tempId];
            object.str_id = tempId;
            
            NSString *tempsublevel = [results stringForColumn:@"sublevel"];
            [arrRowSublevelPadVc addObject:tempsublevel];
            object.str_Sublevel = tempsublevel;
            
            NSString *tempTitle = [results stringForColumn:@"newrem"];
            tempTitle = [tempTitle stringByReplacingOccurrencesOfString:@"|" withString:@","];
            [arrRowTitlePadVc addObject:tempTitle];
            object.str_title = tempTitle;
            
            
            NSString *tempCategory = [results stringForColumn:@"categoy"];
            tempCategory = [tempCategory stringByReplacingOccurrencesOfString:@"|" withString:@","];
            [arrRowSubTitlePadVc addObject:tempCategory];
            object.str_UnderBtn = tempCategory;
           
            
            
            NSString *tempIntencity = [results stringForColumn:@"Intensity"];
            [arrRowIntencityPadVc addObject:tempIntencity];
            object.str_IntencityValue = tempIntencity;

            
            NSString *temparRemedies = [results stringForColumn:@"Remedies"];
            NSString *filteredString = [self filterRemediesString:temparRemedies];
            
            [arrRowRemediesPadVc addObject:filteredString];
            object.str_txtViewText = filteredString;
            
            
           
            [arrAllObjects addObject:object];
      
        }
    
    
    if(arrRowTitlePadVc.count >0)
    {
         [_tableViewKentPadVC reloadData];
         [progressView hide:YES];
    }
    else
    {
        alert_NoData = [[UIAlertView alloc]initWithTitle:@"Oops!!! Nothing inside..." message:nil delegate:self cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
        [alert_NoData show];
        
        
    }
   
  
}
-(BOOL)isSelected_WhereId:(NSString *)_id
{
    BOOL isChecked = NO;
    
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT selected FROM tbl_shifa where _id = %@",_id];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *strselected = [results stringForColumn:@"selected"];
        if([strselected intValue]== 1)
        {
            isChecked = YES;
        }
        else
        {
            isChecked = NO;
        }
        
    }
    return isChecked;
}

-(void)getCheckboxSelected_Ids
{
    [app.arrCheckBoxClikedId removeAllObjects];
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id FROM tbl_shifa where selected ='1'"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *strTemp = [results stringForColumn:@"_id"];
        [app.arrCheckBoxClikedId addObject:strTemp];
    }
}


- (IBAction)onClickBackPadVC:(id)sender{
    
    [self.navigationController popViewControllerAnimated:NO];
    
}
- (IBAction)onClickSearchPadVC:(id)sender{
    
     [_tableViewKentPadVC scrollRectToVisible:CGRectMake(0, 0, 1, 1) animated:NO];
    [self.searchDisplayController.searchBar becomeFirstResponder];
    
}
- (IBAction)onClickOverViewPadVC:(id)sender{
    
    
}
- (IBAction)onClickPadBtnPadVC:(id)sender{
    
    
}

-(NSString *)filterRemediesString:(NSString *)tempRemedies{
    
    NSString *filteredString =@"";
    
    if(IS_SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0")){
        
        if(tempRemedies.length>0)
        {
            NSArray *arr_sepColon = [tempRemedies componentsSeparatedByString:@":"];
            for(NSString *str in arr_sepColon)
            {
                if(str.length>0){
                    NSArray *arr_sepSemicolon = [str componentsSeparatedByString:@","];
                    if([[arr_sepSemicolon objectAtIndex:1] isEqualToString:@"1"])
                    {
                        filteredString =  [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='red'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
                        
                    }
                    if([[arr_sepSemicolon objectAtIndex:1] isEqualToString:@"2"])
                    {
                        filteredString =   [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='blue'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
                    }
                    if([[arr_sepSemicolon objectAtIndex:1] isEqualToString:@"3"])
                    {
                        filteredString =  [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='black'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
                    }
                }
            }
        }
        
    }
    else
    {
        //Remedies For Less than IOS 7  which Not Allow HTML string in UITextView so we send Black String
        NSCharacterSet *okCharacterSet = [NSCharacterSet characterSetWithCharactersInString:@"abcdefghijklmnopqrstuvwxyz,-ABCDEFGHIJKLMNOPQRSTUVWXYZ"];
        filteredString = [[tempRemedies componentsSeparatedByCharactersInSet:
                           [okCharacterSet invertedSet]]
                          componentsJoinedByString:@""];
    }
    
    
    
    return filteredString;
}

#pragma mark - Search Delegate Methods

- (void)filterContentForSearchText:(NSString*)searchText scope:(NSString*)scope
{
    
    NSPredicate *resultPredicate = [NSPredicate predicateWithFormat:@"str_title contains[c] %@", searchText];
    arrSearchResults = [arrAllObjects filteredArrayUsingPredicate:resultPredicate];
    
    /*NOTE_J := Basically, a predicate is an expression that returns a Boolean value (true or false). You specify the search criteria in the format of NSPredicate and use it to filter data in the array. As the search is on the str_title of recipe, we specify the predicate as “str_title contains[c] %@”. The “str_title” refers to the name property of the Recipe object. NSPredicate supports a wide range of filters including:
     
     BEGINSWITH
     ENDSWITH
     LIKE
     MATCHES
     CONTAINS
     
     Here we choose to use the “contains” filter. The operator “[c]” means the comparison is case-insensitive.
     */
    
}
-(BOOL)searchDisplayController:(UISearchDisplayController *)controller shouldReloadTableForSearchString:(NSString *)searchString
{
    [self filterContentForSearchText:searchString
                               scope:[[self.searchDisplayController.searchBar scopeButtonTitles]
                                      objectAtIndex:[self.searchDisplayController.searchBar
                                                     selectedScopeButtonIndex]]];
    
    return YES;
}
- (void)searchBarTextDidBeginEditing:(UISearchBar *)searchBar {
    
    
    isSearching = YES;
   
}
- (void)searchBarCancelButtonClicked:(UISearchBar *)searchBar {
    
    isSearching = NO;
   
}


- (void)searchBarSearchButtonClicked:(UISearchBar *)searchBar {
    NSLog(@"Search Clicked");
    
}

-(void)searchDisplayController:(UISearchDisplayController *)controller didShowSearchResultsTableView:(UITableView *)tableView {
    
    
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        tableView.backgroundColor = [UIColor whiteColor];
        //tableView.frame=CGRectZero;//This must be set to prevent the result tables being shown
        
        _tableViewKentPadVC.hidden  = YES;
        tableView.frame           = _tableViewKentPadVC.frame;
        //tableView.frame         = CGRectMake(200, 0, 200, 400);
    }
    
}
-(void)searchDisplayController:(UISearchDisplayController *)controller didHideSearchResultsTableView:(UITableView *)tableView
{
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        _tableViewKentPadVC.hidden = NO;
        tableView.hidden = YES;
    }
    
}


#pragma mark -Table View

-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{

    return 1;
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    if (tableView == self.searchDisplayController.searchResultsTableView || isSearching==YES)
    {
        return arrSearchResults.count;
    }
    else
    {
        return arrAllObjects.count;
    }
    
}
-(UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    
    static NSString * CellIdentifire=@"ExpandebleCellPad";
    kentVC2Cell * cell=[_tableViewKentPadVC dequeueReusableCellWithIdentifier:CellIdentifire];
    
    if(cell == nil)
    {
        
        cell = [[kentVC2Cell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifire];
        
    }
    else {
        for (UIView* tempView in cell.contentView.subviews) {
            
            if([tempView isKindOfClass:[MICheckBox class]])
            {
                [tempView removeFromSuperview];
            }
        }
    }

    
    
    
    SearchObjectClass *Obj = nil;
    if (tableView == self.searchDisplayController.searchResultsTableView)
    {
        Obj = [arrSearchResults objectAtIndex:indexPath.row];
        
    } else
    {
        Obj = [arrAllObjects objectAtIndex:indexPath.row];
    }
    
    
    
    
   
    cell.lbl_title.text = Obj.str_title;
    cell.lbl_underBtn.text = Obj.str_UnderBtn;
    cell.lbl_IntencityValue.text = Obj.str_IntencityValue;
    
    
    
    
    //SET TxtVIEW PROPERTIES
    //NSLog(@"Tview Text WhenExpandingCell==%@",Obj.str_txtViewText);
     cell.txtView_KentVc1.userInteractionEnabled=NO;
   
   if (SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        
        NSAttributedString *attributedString = [[NSAttributedString alloc] initWithData:[Obj.str_txtViewText dataUsingEncoding:NSUnicodeStringEncoding] options:@{ NSDocumentTypeDocumentAttribute: NSHTMLTextDocumentType } documentAttributes:nil error:nil];
        cell.txtView_KentVc1.attributedText = attributedString;
        
        CGSize AvrodhkarakSize = CGSizeMake(320, 1000);
        CGSize textViewHeight = [self text:cell.txtView_KentVc1.text sizeWithFont:[UIFont systemFontOfSize:12] constrainedToSize:AvrodhkarakSize];
        CGRect newFrame=cell.txtView_KentVc1.frame;
        newFrame.size.height=textViewHeight.height+10;
        cell.txtView_KentVc1.frame=newFrame;
        
    }
    else
    {
         cell.txtView_KentVc1.text = Obj.str_txtViewText;
        
        CGSize textViewSize = [cell.txtView_KentVc1 sizeThatFits:CGSizeMake(cell.txtView_KentVc1.frame.size.width, FLT_MAX)];
        CGRect newFrame=cell.txtView_KentVc1.frame;
        newFrame.size.height=textViewSize.height;
        cell.txtView_KentVc1.frame=newFrame;
        
        
    }

    
    //SET BTN PROPERTIES
    NSString *strSublevel = Obj.str_Sublevel ;
    NSString *strTempImageName = [strSublevel intValue]==0?@"ic_kent_overview.png":@"icon-58.png";
    cell.btn_ImageView.tag = indexPath.row;
    [cell.btn_ImageView setBackgroundImage:[UIImage imageNamed:strTempImageName] forState:UIControlStateNormal];
  
    
    
    //SET CheckBox
    //SET CheckBox
    MICheckBox *checkBox =[[MICheckBox alloc]init];
    checkBox.tag=[Obj.str_id intValue];
    [cell.contentView addSubview:checkBox];
    
    //Prechecking MICheckBox
    if([app.arrCheckBoxClikedId containsObject:Obj.str_id])
    {
        
        [checkBox checkBoxClicked:checkBox];
    }

    
    
    //SET CELL LABLE DATA
    NSString *strIntencityTemp = Obj.str_IntencityValue;
    if([strIntencityTemp intValue]!=0){
        
        checkBox.hidden = NO;
        cell.btn_CheckboxDisable.hidden = YES;
      
        cell.lbl_IntencityValue.text = Obj.str_IntencityValue;
        cell.btn_CheckboxDisable.frame = CGRectMake(cell.btn_CheckboxDisable.frame.origin.x, cell.btn_CheckboxDisable.frame.origin.y, 40, 40);
        cell.btn_CheckboxDisable.center = CGPointMake(cell.btn_CheckboxDisable.center.x, cell.lbl_IntencityValue.center.y) ;
        checkBox.frame = cell.btn_CheckboxDisable.frame;

        
    }
    else
    {
        cell.lbl_IntencityValue.text = @"";
        checkBox.hidden = YES;
        cell.btn_CheckboxDisable.hidden = NO;
        cell.btn_CheckboxDisable.frame = CGRectMake(cell.btn_CheckboxDisable.frame.origin.x, cell.btn_CheckboxDisable.frame.origin.y, 22 ,22);
        cell.btn_CheckboxDisable.center = CGPointMake(cell.btn_CheckboxDisable.center.x, cell.btn_ImageView.center.y) ;
        
        NSMutableAttributedString *attributeString = [[NSMutableAttributedString alloc] initWithString:cell.lbl_title.text];
        [attributeString addAttribute:NSUnderlineStyleAttributeName
                                value:[NSNumber numberWithInt:1]
                                range:(NSRange){0,[attributeString length]}];
        
        [attributeString addAttribute:NSUnderlineStyleAttributeName value:[NSNumber numberWithInt:1] range:(NSRange){0,[attributeString length]}];
        [attributeString addAttribute:NSForegroundColorAttributeName value:[UIColor blackColor] range:NSMakeRange(0,[attributeString length])];
        [attributeString addAttribute:NSFontAttributeName value:[UIFont fontWithName:@"HelveticaNeue-Bold" size:12.0] range:NSMakeRange(0, [attributeString length])];
      //cell.lbl_title.attributedText = [attributeString copy];
        cell.lbl_title.attributedText = attributeString;
        
    }
    

    return cell;
    
}
-(CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath
{
  
    SearchObjectClass *NSObj = nil;
    
    if (isSearching==YES )
    {
        
        NSObj = [arrSearchResults objectAtIndex:indexPath.row];
        
    }
    else
    {
        NSObj = [arrAllObjects objectAtIndex:indexPath.row];
        
    }


    
    txt_ToCalculateHeight.userInteractionEnabled=NO;

    
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        
        NSAttributedString *attributedString = [[NSAttributedString alloc] initWithData:[NSObj.str_txtViewText dataUsingEncoding:NSUnicodeStringEncoding] options:@{ NSDocumentTypeDocumentAttribute: NSHTMLTextDocumentType } documentAttributes:nil error:nil];
        txt_ToCalculateHeight.attributedText = attributedString;
        
        CGSize AvrodhkarakSize = CGSizeMake(320, 1000);
        CGSize textViewHeight = [self text:txt_ToCalculateHeight.text sizeWithFont:[UIFont systemFontOfSize:12] constrainedToSize:AvrodhkarakSize];
        CGRect newFrame=txt_ToCalculateHeight.frame;
        newFrame.size.height=textViewHeight.height+10;
        txt_ToCalculateHeight.frame=newFrame;
        
    }
    else
    {
        
        txt_ToCalculateHeight.text = NSObj.str_txtViewText;
        
        CGSize textViewSize = [txt_ToCalculateHeight sizeThatFits:CGSizeMake(txt_ToCalculateHeight.frame.size.width, FLT_MAX)];
        CGRect newFrame=txt_ToCalculateHeight.frame;
        newFrame.size.height=textViewSize.height;
        txt_ToCalculateHeight.frame=newFrame;

    }

    

    if(txt_ToCalculateHeight.text.length>0)
   {
        return 65+txt_ToCalculateHeight.frame.size.height;
   }
   else
   {
       return 65;
   }

}

- (CGSize)text:(NSString *)text sizeWithFont:(UIFont *)font constrainedToSize:(CGSize)size
{
    if (SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        CGRect frame = [text boundingRectWithSize:size
                                          options:(NSStringDrawingUsesLineFragmentOrigin | NSStringDrawingUsesFontLeading)
                                       attributes:@{NSFontAttributeName:font}
                                          context:nil];
        return frame.size;
    }
    else
    {
        return [text sizeWithFont:font constrainedToSize:size];
    }
    
}


#pragma mark - Alert Delegate Methods
-(void)HideAlertCustomeMethod:(UIAlertView *)alertToBeHidden
{
    [alertToBeHidden dismissWithClickedButtonIndex:[alertToBeHidden cancelButtonIndex] animated:YES];
    
}
-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    if(alertView == alert_NoData)
    {
        if(buttonIndex == 0)
        {
          [self.navigationController popViewControllerAnimated:NO];
            
        }
    
    }
  
}


@end
