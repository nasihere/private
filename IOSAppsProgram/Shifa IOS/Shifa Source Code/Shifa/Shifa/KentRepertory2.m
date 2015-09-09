//
//  KentRepertory2.m
//  Shifa
//
//  Created by My Mac on 4/21/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "KentRepertory2.h"
#import "kentVC2Cell.h"
#import "SearchObjectClass.h"
#import "PadViewController.h"
#import "MICheckBox.h"
#import <QuartzCore/QuartzCore.h>

@interface KentRepertory2 ()

@end

@implementation KentRepertory2


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
       
        
        
    }
    return self;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
    [self allocInitCustomMethod];
    progressView = [[MBProgressHUD alloc]initWithView:self.view];
    progressView.labelText = @"Please wait";
    [self.view addSubview:progressView];
    [progressView show:YES];
    [self getCheckboxSelected_Ids];
    
    _tableViewKentVC2.delegate =self;
    _tableViewKentVC2.dataSource = self;
    if ([_tableViewKentVC2 respondsToSelector:@selector(setSeparatorInset:)])
    {
        [_tableViewKentVC2 setSeparatorInset:UIEdgeInsetsZero];
    }
   

    [self performSelector:@selector(getKentRepertoryListvc2WhereCategory:) withObject:app.strSelectedRowStringAtKentVC1 afterDelay:0.1];
     _tableViewKentVC2.contentOffset = CGPointMake(0, self.searchDisplayController.searchBar.frame.size.height+2);

}
-(void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    
         isSearchByViewVisible = NO;
         [_view_SearchBy setAlpha:0.0];
    
        _view_SearchBy.layer.cornerRadius=2.0f;
         _view_SearchBy.layer.borderColor=[[UIColor blackColor] CGColor];
         _view_SearchBy.clipsToBounds = YES;
         _view_SearchBy.layer.borderWidth=2;
    
    
    
    [_tableViewKentVC2 reloadData];
   
}
-(void)allocInitCustomMethod
{
    
    arrRowId                    =[[NSMutableArray alloc]init];
    arrSublevel                 =[[NSMutableArray alloc]init];
    arrRowTitle                 =[[NSMutableArray alloc]init];
    arrRowSubTitle              =[[NSMutableArray alloc]init];
    arrRowIntencity             =[[NSMutableArray alloc]init];
    arrRowRemedies              =[[NSMutableArray alloc]init];
    dix_BtnTagRowHeight         =[[NSMutableDictionary alloc]init];
    arrSearchResults            =[[NSArray alloc]init];
    arrAllObjects               =[[NSMutableArray alloc]init];
    
}
-(void)viewWillDisappear:(BOOL)animated
{
    [super viewWillDisappear:animated];
    
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    
}

#pragma mark -Actions
-(void)getKentRepertoryListvc2WhereCategory:(NSString *)category
{
    
    BOOL isRecordPresent = NO;
    isRecordPresent = [self IsRecordisPresentIn_tblShifa_WhereCategory:category];
    
    if(isRecordPresent == YES)
    {
        
        [arrRowId      removeAllObjects];
        [arrSublevel    removeAllObjects];
        [arrRowTitle     removeAllObjects];
        [arrRowSubTitle   removeAllObjects];
        [arrRowIntencity   removeAllObjects];
        [arrRowRemedies     removeAllObjects];
        [dix_BtnTagRowHeight  removeAllObjects];
        arrSearchResults       =nil;
        [arrAllObjects           removeAllObjects];
        
        
        
        
        NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id,Name,categoy,Intensity,Remedies,newrem,sublevel,selected FROM tbl_shifa where categoy ='%@' COLLATE NOCASE order by newrem",category];
        NSLog(@"Quary At KentReportery2==%@",QuaryString);
        FMResultSet *results = [app.database executeQuery:QuaryString];
        while([results next]) {
            
            
            SearchObjectClass *object = [SearchObjectClass new];
            
            
            NSString *tempId = [results stringForColumn:@"_id"];
            [arrRowId addObject:tempId];
            object.str_id =tempId;
            
            NSString *tempsublevel = [results stringForColumn:@"sublevel"];
            [arrSublevel addObject:tempsublevel];
            object.str_Sublevel =tempsublevel;
            
            NSString *tempTitle = [results stringForColumn:@"Name"];
            [arrRowTitle addObject:tempTitle];
            object.str_title = tempTitle;
            
            
            NSString *tempCategory = [results stringForColumn:@"categoy"];
            tempCategory = [tempCategory stringByReplacingOccurrencesOfString:@"|" withString:@","];
            [arrRowSubTitle addObject:tempCategory];
            object.str_UnderBtn = tempCategory;
            
            
            NSString *tempIntencity = [results stringForColumn:@"Intensity"];
            [arrRowIntencity addObject:tempIntencity];
            object.str_IntencityValue = tempIntencity;
            
            
             NSString *temparRemedies = [results stringForColumn:@"Remedies"];
             NSString *filteredString = [self filterRemediesString:temparRemedies];

            
            
            [arrRowRemedies addObject:filteredString];
            object.str_txtViewText = filteredString;
            [arrAllObjects addObject:object];
            
        }
        
        //searchBarCancelButtonClicked
        strPreviousQuaryCategoryString = category;
        [progressView hide:YES];
        [_tableViewKentVC2 reloadData];
        
        
    }
    
}

-(void)getKentRepertoryListvc2_SearchByCategoryNsubCategory_WhereNewrem:(NSString *)newrem
{
    
        [arrRowId      removeAllObjects];
        [arrSublevel    removeAllObjects];
        [arrRowTitle     removeAllObjects];
        [arrRowSubTitle   removeAllObjects];
        [arrRowIntencity   removeAllObjects];
        [arrRowRemedies     removeAllObjects];
        [dix_BtnTagRowHeight  removeAllObjects];
        arrSearchResults       =nil;
        [arrAllObjects           removeAllObjects];
    
        
        NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id,Name,categoy,Intensity,Remedies,newrem,sublevel,selected FROM tbl_shifa where newrem LIKE '%@' COLLATE NOCASE order by newrem, Name",newrem];
        NSLog(@"Quary At KentReportery SearchBy cat&SubCategory==%@",QuaryString);
        FMResultSet *results = [app.database executeQuery:QuaryString];
        while([results next]) {
            
            
            SearchObjectClass *object = [SearchObjectClass new];
            
            
            NSString *tempId = [results stringForColumn:@"_id"];
            [arrRowId addObject:tempId];
            object.str_id =tempId;
            
            NSString *tempsublevel = [results stringForColumn:@"sublevel"];
            [arrSublevel addObject:tempsublevel];
            object.str_Sublevel =tempsublevel;
            
            NSString *tempTitle = [results stringForColumn:@"Name"];
            [arrRowTitle addObject:tempTitle];
            object.str_title = tempTitle;
            
            
            NSString *tempCategory = [results stringForColumn:@"categoy"];
            tempCategory = [tempCategory stringByReplacingOccurrencesOfString:@"|" withString:@","];
            [arrRowSubTitle addObject:tempCategory];
            object.str_UnderBtn = tempCategory;
            
            
            NSString *tempIntencity = [results stringForColumn:@"Intensity"];
            [arrRowIntencity addObject:tempIntencity];
            object.str_IntencityValue = tempIntencity;
            
            
            NSString *temparRemedies = [results stringForColumn:@"Remedies"];
            NSString *filteredString = [self filterRemediesString:temparRemedies];
            
            
            
            [arrRowRemedies addObject:filteredString];
            object.str_txtViewText = filteredString;
            [arrAllObjects addObject:object];
            
        }
        
        //searchBarCancelButtonClicked
        //strPreviousQuaryCategoryString = category;
        [progressView hide:YES];
        [_tableViewKentVC2 reloadData];
    
}

-(BOOL)IsRecordisPresentIn_tblShifa_WhereCategory:(NSString *)category{

    BOOL isRecordExists = NO;
    NSMutableArray *arrTitleTemp = [[NSMutableArray alloc]init];

    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id,Name,categoy,Intensity,Remedies,newrem,sublevel,selected FROM tbl_shifa where categoy ='%@' COLLATE NOCASE order by newrem",category];
  

    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {

        NSString *tempTitle = [results stringForColumn:@"Name"];
        [arrTitleTemp addObject:tempTitle];

    }
    if(arrTitleTemp.count>0)
    {
     isRecordExists=YES;
    }
    else
    {
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Oops!!! Nothing inside..." message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
    }
    return isRecordExists;

}
-(BOOL)isSelected_WhereId:(NSString *)_id
{
    BOOL isChecked = NO;
    
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT selected FROM tbl_shifa where _id =%@",_id];
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



- (IBAction)onClickBackVC2:(id)sender
{
     NSRange character = [strPreviousQuaryCategoryString rangeOfString:@"|"];
    
            if (character.location == NSNotFound)
            {
               [self.navigationController popViewControllerAnimated:NO];
            }
            else
            {
                NSMutableArray *arrTemp = [[strPreviousQuaryCategoryString componentsSeparatedByString:@"|"] mutableCopy];
                [arrTemp removeLastObject];
              NSString *ClipedLastCategoryString = @"";
                for(int i=0; i<arrTemp.count; i++)
                {
                   
                    if(i!=arrTemp.count-1)
                    {
                        ClipedLastCategoryString = [ClipedLastCategoryString stringByAppendingString:[NSString stringWithFormat:@"%@|",[arrTemp objectAtIndex:i]]];
                    }
                    else
                    {
                         ClipedLastCategoryString = [ClipedLastCategoryString stringByAppendingString:[NSString stringWithFormat:@"%@",[arrTemp objectAtIndex:i]]];
                    }
                    
                    
                }
                
                
                [self getKentRepertoryListvc2WhereCategory:ClipedLastCategoryString];
                

            }
}

- (IBAction)onClickOverViewVC2:(id)sender{
}

- (IBAction)onClickPadBtn:(id)sender{
    
    app.strPassStringKentVC2toPadVC = @"";
    app.strPassStringKentVC2toPadVC = [arrRowSubTitle objectAtIndex:0];
    app.strPassStringKentVC2toPadVC = [app.strPassStringKentVC2toPadVC stringByReplacingOccurrencesOfString:@"," withString:@"|"];
    
    PadViewController *padVC = [self.storyboard instantiateViewControllerWithIdentifier:@"PadViewController"];
    [self.navigationController pushViewController:padVC animated:NO];
    
}
- (IBAction)onClickSearchVC2:(id)sender{
    
    [self show_Hide_SearchByView];
    
    
}

- (IBAction)onClick_SelfCategoriesBtn:(id)sender {
    
    
    [self show_Hide_SearchByView];
    
    [_tableViewKentVC2 scrollRectToVisible:CGRectMake(0, 0, 1, 1) animated:NO];
    [self.searchDisplayController.searchBar becomeFirstResponder];
    
    
    
}

- (IBAction)onClick_SelfCategoriesNsubCategoriesBtn:(id)sender {
    
    [self show_Hide_SearchByView];
    
    NSString *str_ToPass = [NSString stringWithFormat:@"%@%@",strPreviousQuaryCategoryString,@"%"];
    [self performSelector:@selector(getKentRepertoryListvc2_SearchByCategoryNsubCategory_WhereNewrem:) withObject:str_ToPass afterDelay:0.1];
   
    
    [_tableViewKentVC2 scrollRectToVisible:CGRectMake(0, 0, 1, 1) animated:NO];
    [self.searchDisplayController.searchBar becomeFirstResponder];
    
    
}

-(void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event
{
    isSearchByViewVisible = NO;
    [_view_SearchBy setAlpha:0.0];
    
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
                        filteredString =  [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='black'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
                        
                    }
                    if([[arr_sepSemicolon objectAtIndex:1] isEqualToString:@"2"])
                    {
                        filteredString =   [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='blue'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
                    }
                    if([[arr_sepSemicolon objectAtIndex:1] isEqualToString:@"3"])
                    {
                        filteredString =  [filteredString stringByAppendingString:[NSString stringWithFormat:@"<font color='red'>%@</font>,",[arr_sepSemicolon objectAtIndex:0]]];
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


-(void)show_Hide_SearchByView{
    
    if (!isSearchByViewVisible)
    {
        
        [UIView animateWithDuration:0.3
                              delay:0.0
                            options:UIViewAnimationOptionCurveEaseIn
                         animations:^{
                             [_view_SearchBy setAlpha:1.0];
                         }
                         completion:^(BOOL finished){
                         }
         ];
    }
    else
    {
        
        
        [UIView animateWithDuration:0.3
                              delay:0.0
                            options:UIViewAnimationOptionCurveEaseOut
                         animations:^{
                             [_view_SearchBy setAlpha:0.0];
                         }
                         completion:^(BOOL finished){
                         }
         ];
    }
    
    isSearchByViewVisible = !isSearchByViewVisible;
    
    
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
   

}
- (void)searchBarSearchButtonClicked:(UISearchBar *)searchBar {
    
    NSLog(@"Search Clicked");
    
}

- (void)searchBarCancelButtonClicked:(UISearchBar *)searchBar {
    
   
    DLog(@"Cancel Cliked");
   
}

-(void)searchDisplayController:(UISearchDisplayController *)controller didShowSearchResultsTableView:(UITableView *)tableView {
    
   
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        
        //Make Seprator line full
        if ([tableView respondsToSelector:@selector(setSeparatorInset:)]) {
            [tableView setSeparatorInset:UIEdgeInsetsZero];
        }
        
        tableView.backgroundColor = [UIColor whiteColor];
        //tableView.frame=CGRectZero;//This must be set to prevent the result tables being shown
        _tableViewKentVC2.hidden  = YES;
        tableView.frame           = _tableViewKentVC2.frame;
        //tableView.frame         = CGRectMake(200, 0, 200, 400);
    }

}
-(void)searchDisplayController:(UISearchDisplayController *)controller didHideSearchResultsTableView:(UITableView *)tableView
{
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        _tableViewKentVC2.hidden = NO;
        tableView.hidden = YES;
    }
    
}


#pragma mark -Table View Delegate Methods

-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return 1;
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    
    if (tableView == self.searchDisplayController.searchResultsTableView )
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
    
    isSearchByViewVisible = NO;
    [_view_SearchBy setAlpha:0.0];
    static NSString * CellIdentifire=@"ExpandebleCell";
    kentVC2Cell * cellCustome= (kentVC2Cell *)[_tableViewKentVC2 dequeueReusableCellWithIdentifier:CellIdentifire];
   
    
    if(cellCustome == nil)
    {
        
        cellCustome = [[kentVC2Cell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifire];
       
    }
    else{
        
        for (UIView* tempView in cellCustome.contentView.subviews) {
        
            if([tempView isKindOfClass:[MICheckBox class]])
            {
                [tempView removeFromSuperview];
            }
        
        }
        
    }
    
    
    
    
    SearchObjectClass *Obj = nil;
    if (tableView == self.searchDisplayController.searchResultsTableView && arrSearchResults.count>0)
    {
        Obj = [arrSearchResults objectAtIndex:indexPath.row];
    
    } else
    {
        Obj = [arrAllObjects objectAtIndex:indexPath.row];
    }
   
         cellCustome.lbl_title.text = Obj.str_title;
         cellCustome.lbl_underBtn.text = Obj.str_UnderBtn;
         cellCustome.lbl_IntencityValue.text = Obj.str_IntencityValue;
    
    
   

    //SET TxtVIEW PROPERTIES
    //NSLog(@"Tview Text WhenExpandingCell==%@",Obj.str_txtViewText);
    cellCustome.txtView_KentVc1.userInteractionEnabled=NO;
    cellCustome.txtView_KentVc1.hidden = YES;
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        
        NSAttributedString *attributedString = [[NSAttributedString alloc] initWithData:[Obj.str_txtViewText dataUsingEncoding:NSUnicodeStringEncoding] options:@{ NSDocumentTypeDocumentAttribute: NSHTMLTextDocumentType } documentAttributes:nil error:nil];
        cellCustome.txtView_KentVc1.attributedText = attributedString;
        
        
        CGSize AvrodhkarakSize = CGSizeMake(320, 1000);
        CGSize textViewHeight = [self text:cellCustome.txtView_KentVc1.text sizeWithFont:[UIFont systemFontOfSize:12] constrainedToSize:AvrodhkarakSize];
        CGRect newFrame=cellCustome.txtView_KentVc1.frame;
        newFrame.size.height=textViewHeight.height+10;
        cellCustome.txtView_KentVc1.frame=newFrame;
        
    }
    else
    {
        
        cellCustome.txtView_KentVc1.text = Obj.str_txtViewText;
        
        CGSize textViewSize = [cellCustome.txtView_KentVc1 sizeThatFits:CGSizeMake(cellCustome.txtView_KentVc1.frame.size.width, FLT_MAX)];
        CGRect newFrame=cellCustome.txtView_KentVc1.frame;
        newFrame.size.height=textViewSize.height;
        cellCustome.txtView_KentVc1.frame=newFrame;
        
    }


    
    //SET BTN PROPERTIES
    NSString *strSublevel = Obj.str_Sublevel;
    NSString *strTempImageName = [strSublevel intValue]==0? @"ic_cateogry.png" : @"icon-58.png";
  
     cellCustome.btn_ImageView.tag = [Obj.str_id intValue];
    [cellCustome.btn_ImageView setBackgroundImage:[UIImage imageNamed:strTempImageName] forState:UIControlStateNormal];
    NSString *rowHeight =[NSString stringWithFormat:@"%f",75+cellCustome.txtView_KentVc1.frame.size.height];
    if(Obj.str_txtViewText.length>0){
      [cellCustome.btn_ImageView setTitle:rowHeight forState:UIControlStateNormal];
    }else{
       [cellCustome.btn_ImageView setTitle:@"75" forState:UIControlStateNormal];
    }
   
    
  [cellCustome.btn_ImageView addTarget:self action:@selector(PlusBtnCliked:) forControlEvents:UIControlEventTouchUpInside];
    
    
    //CHANGE IMAGE OF PLUSH BTN
    NSArray *allkey = [dix_BtnTagRowHeight allKeys];
    if([allkey containsObject:Obj.str_id] && [strSublevel intValue]==0)
    {
         cellCustome.txtView_KentVc1.hidden = NO;
        [cellCustome.btn_ImageView setBackgroundImage:[UIImage imageNamed:@"ic_cateogryminus.png"] forState:UIControlStateNormal];
    }
    else if([strSublevel intValue]==0)
    {
        [cellCustome.btn_ImageView setBackgroundImage:[UIImage imageNamed:@"ic_cateogry.png"] forState:UIControlStateNormal];
    }

    
    //SET CheckBox
    MICheckBox *checkBox =[[MICheckBox alloc]init];
    checkBox.tag=[Obj.str_id intValue];
    [cellCustome.contentView addSubview:checkBox];
    
    //Prechecking MICheckBox
    if([app.arrCheckBoxClikedId containsObject:Obj.str_id])
    {

        [checkBox checkBoxClicked:checkBox];
    }
   
    
    

    //SET CELL LABLE DATA
    NSString *strIntencityTemp = Obj.str_IntencityValue;
    if([strIntencityTemp intValue]!=0){
     
        checkBox.hidden = NO;
        cellCustome.btn_CheckboxDisable.hidden = YES;
        cellCustome.lbl_IntencityValue.text = Obj.str_IntencityValue;
        cellCustome.btn_CheckboxDisable.frame = CGRectMake(cellCustome.btn_CheckboxDisable.frame.origin.x, cellCustome.btn_CheckboxDisable.frame.origin.y, 40, 40);
        cellCustome.btn_CheckboxDisable.center = CGPointMake(cellCustome.btn_CheckboxDisable.center.x, cellCustome.lbl_IntencityValue.center.y) ;
        checkBox.frame = cellCustome.btn_CheckboxDisable.frame;
    
    }
    else
    {
        cellCustome.lbl_IntencityValue.text = @"";
        checkBox.hidden = YES;
        cellCustome.btn_CheckboxDisable.hidden = NO;
        cellCustome.btn_CheckboxDisable.frame = CGRectMake(cellCustome.btn_CheckboxDisable.frame.origin.x, cellCustome.btn_CheckboxDisable.frame.origin.y, 22 ,22);
        cellCustome.btn_CheckboxDisable.center = CGPointMake(cellCustome.btn_CheckboxDisable.center.x, cellCustome.btn_ImageView.center.y) ;

       
 
    }
   return cellCustome;
}

-(CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSArray *allkey = [dix_BtnTagRowHeight allKeys];
    SearchObjectClass *NSObj = nil;
    
    if (self.searchDisplayController.active && allkey.count>0)
    {
        if(indexPath.row<arrSearchResults.count){
        NSObj = [arrSearchResults objectAtIndex:indexPath.row];
        }
    }
    else
    {
        NSObj = [arrAllObjects objectAtIndex:indexPath.row];
        
    }



    if([allkey containsObject:NSObj.str_id])
    {
      
       return [[dix_BtnTagRowHeight valueForKey:NSObj.str_id]floatValue];
    
    }
    else
    {
        return 75.0;
        
    }
    
    
  //  return 175.0;

}

-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
   
    isSearchByViewVisible = NO;
    [_view_SearchBy setAlpha:0.0];
    
    SearchObjectClass *NSObj = nil;
     _tableViewKentVC2.hidden = NO;
    
    if (self.searchDisplayController.active)
    {
        if(indexPath.row<arrSearchResults.count){
            indexPath = [self.searchDisplayController.searchResultsTableView indexPathForSelectedRow];
            NSObj = [arrSearchResults objectAtIndex:indexPath.row];
           [self.searchDisplayController.searchResultsTableView deselectRowAtIndexPath:indexPath animated:NO];
        }
        
    }
    else
    {
        indexPath = [_tableViewKentVC2 indexPathForSelectedRow];
        NSObj = [arrAllObjects objectAtIndex:indexPath.row];
        [_tableViewKentVC2 deselectRowAtIndexPath:indexPath animated:NO];
    }
    
   
   
    NSString *strCellTitleCategory = NSObj.str_title;
  
    
     [self.searchDisplayController setActive:NO animated:NO];
     [self getKentRepertoryListvc2WhereCategory:[NSString stringWithFormat:@"%@|%@",strPreviousQuaryCategoryString,strCellTitleCategory]];
    
    
   
    
}

-(void)PlusBtnCliked:(id)sender{
    
    UIButton *clikedBtn = (UIButton *)sender;
    NSString *btnTag = [NSString stringWithFormat:@"%d",clikedBtn.tag];
    NSArray *allkey = [dix_BtnTagRowHeight allKeys];
   
    
    if([allkey containsObject:btnTag])
    {
       
        [dix_BtnTagRowHeight removeObjectForKey:btnTag];

    }
    else
    {
      [dix_BtnTagRowHeight setValue:clikedBtn.currentTitle forKey:btnTag];
     
    }
    
    if (self.searchDisplayController.active)
    {
      [arrAllObjects removeAllObjects];
      arrAllObjects = [NSMutableArray arrayWithArray:arrSearchResults];
      [self.searchDisplayController setActive:NO animated:NO];
    }
    [_tableViewKentVC2 reloadData];
    
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


@end
