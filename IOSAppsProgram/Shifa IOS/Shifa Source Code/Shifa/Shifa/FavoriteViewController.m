//
//  FavoriteViewController.m
//  Shifa
//
//  Created by My Mac on 4/29/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "FavoriteViewController.h"
#import "CellClass_BottomCollectionVIew.h"
#import "Cell_OrderBySumOfGradient.h"
#import "DetailDescriptionOfRemedies.h"


@interface FavoriteViewController ()

@end

@implementation FavoriteViewController

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
    
}

#pragma mark - View LifeCycle

- (void)viewDidLoad
{
    [super viewDidLoad];
	app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
    saveAsView  = [[SaveAsScreen alloc] initWithNibName:@"SaveAsScreen" bundle:[NSBundle mainBundle]];
    saveAsView.SaveAsScreenDelegateObj = self;
    [saveAsView.view setAlpha:0.0];
   // saveAsView.view.frame = CGRectMake(0, _scrollV_Main.frame.origin.y, _scrollV_Main.frame.size.width, [UIScreen mainScreen].bounds.size.height-_scrollV_Main.frame.origin.y);
     saveAsView.view.frame = CGRectMake(0,0, [UIScreen mainScreen].bounds.size.width, [UIScreen mainScreen].bounds.size.height);
    [self.view addSubview:saveAsView.view];
    
    
    progressView = [[MBProgressHUD alloc]initWithView:self.view];
    progressView.labelText = @"Please wait";
    progressView.backgroundColor = [UIColor whiteColor];
    [self.view addSubview:progressView];
    [progressView show:YES];


    
    [self DeleteTable_WhereTableName:@"tbl_report"];
    arrAllRemediesAscending = [[NSMutableArray alloc]init];
    arrAllRemediesForBottomCollectionView = [[NSMutableArray alloc]init];
    arrAllTitles            = [[NSMutableArray alloc]init];
    arr_RemediesSumCount    = [[NSMutableArray alloc]init];
    arr_RemediesSumName     = [[NSMutableArray alloc]init];
    arr_RemediesNumberOfSymtemsCoverdCount = [[NSMutableArray alloc]init];
    arr_RemediesNumberOfSymtemsCoverdName  = [[NSMutableArray alloc]init];
    

   
   
    table_horizontalAtTop   = [[UITableView alloc] init];
    [table_horizontalAtTop setBackgroundColor:[UIColor grayColor]];
    [table_horizontalAtTop.layer setAnchorPoint:CGPointMake(0.0, 0.0)];
    table_horizontalAtTop.transform = CGAffineTransformMakeRotation(M_PI/-2);
    table_horizontalAtTop.showsVerticalScrollIndicator = NO;
    table_horizontalAtTop.userInteractionEnabled = YES;
    table_horizontalAtTop.frame = CGRectMake(150, 40, 10000, 50);
    table_horizontalAtTop.rowHeight = 50.0;
    table_horizontalAtTop.delegate = self;
    table_horizontalAtTop.dataSource = self;
    [_scrollV addSubview:table_horizontalAtTop];

    
    if ([table_horizontalAtTop respondsToSelector:@selector(setSeparatorInset:)])
    {
        [table_horizontalAtTop setSeparatorInset:UIEdgeInsetsZero];
    }
    

    [self performSelector:@selector(FavoriteScreen_Flow) withObject:nil afterDelay:0.5];
  
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.

}

-(void)FavoriteScreen_Flow
{
    
    [self getCheckboxSelected_Ids];
    [self insertInto_tbl_report];
    [self getAllTitlesFrom_tbl_report];
    [self getAllRemediesInAscendingOrderForTopCollectionViewFrom_tbl_report];
    
    [self drawSelectedCheckBox_lblName];
    [self getAllRemediesForPutInto_BottomCollectionView];

    
    [self get_BothArrayForSumOfGradingCollectionView];
    [self get_BothArrayForNumberOfSysmtemsCoveredCollectionView];
    
   //------------------------------------------------------
    
    
    
    CGRect changeFrame          = table_horizontalAtTop.frame;
    changeFrame.origin.x        = lbl_RemoveSymptom.frame.origin.x+lbl_RemoveSymptom.frame.size.width;
    changeFrame.origin.y        = 40;
    changeFrame.size.height     = 5000;
    table_horizontalAtTop.frame = changeFrame;

    
    CGRect changeFrame_ScrollV = _scrollV.frame;
    changeFrame_ScrollV.size.height  =  40*(arrAllTitles.count+2)-30;
    _scrollV.frame = changeFrame_ScrollV;
    

    
    CGRect changeFrame_BottomCollectionView = _collectionview_Bottom.frame;
    changeFrame_BottomCollectionView.origin.y    =lbl_RemoveSymptom.frame.origin.y+lbl_RemoveSymptom.frame.size.height;
    changeFrame_BottomCollectionView.size.height =(40*arrAllTitles.count);
    changeFrame_BottomCollectionView.size.width  =  10000;
    _collectionview_Bottom.frame = changeFrame_BottomCollectionView;
    

    
    CGRect changeFrame_SumCollectionView = _collectionView_SumOfGrading.frame;
    changeFrame_SumCollectionView.origin.y    =_lbl_OrderBySumOfGrading.frame.origin.y+_lbl_OrderBySumOfGrading.frame.size.height;
    changeFrame_SumCollectionView.size.height = 50;
    
    _collectionView_SumOfGrading.frame = changeFrame_SumCollectionView;
    
   
    CGRect changeFrame_NumberCollectionView = _collectionView_NumberOfSymptoms.frame;
    changeFrame_NumberCollectionView.origin.y    =_lbl_OrderByNumberOfSymptoms.frame.origin.y+_lbl_OrderByNumberOfSymptoms.frame.size.height;
    changeFrame_NumberCollectionView.size.height = 50;
    _collectionView_NumberOfSymptoms.frame = changeFrame_NumberCollectionView;
   
   
    
   

[_collectionview_Bottom.collectionViewLayout invalidateLayout];
[_collectionView_SumOfGrading.collectionViewLayout invalidateLayout];
[_collectionView_NumberOfSymptoms.collectionViewLayout invalidateLayout];
    [table_horizontalAtTop reloadData];
    [_collectionview_Bottom reloadData];
    [_collectionView_SumOfGrading reloadData];
    [_collectionView_NumberOfSymptoms reloadData];

    
    
    CGRect changeFrame_lblSum = _lbl_OrderBySumOfGrading.frame;
    changeFrame_lblSum.origin.y  = _scrollV.frame.origin.y+_scrollV.frame.size.height+20;
    _lbl_OrderBySumOfGrading.frame = changeFrame_lblSum;
    CGRect changeFrame_collectionViewSum = _collectionView_SumOfGrading.frame;
    changeFrame_collectionViewSum.origin.y  =  _lbl_OrderBySumOfGrading.frame.origin.y+_lbl_OrderBySumOfGrading.frame.size.height;
    _collectionView_SumOfGrading.frame = changeFrame_collectionViewSum;
    
    CGRect changeFrame_lblNumberOfSymtoms = _lbl_OrderByNumberOfSymptoms.frame;
    changeFrame_lblNumberOfSymtoms.origin.y  =  _collectionView_SumOfGrading.frame.origin.y+_collectionView_SumOfGrading.frame.size.height+20;
    _lbl_OrderByNumberOfSymptoms.frame = changeFrame_lblNumberOfSymtoms;
    CGRect changeFrame_CollectionViewNumSympToms = _collectionView_NumberOfSymptoms.frame;
    changeFrame_CollectionViewNumSympToms.origin.y  =  _lbl_OrderByNumberOfSymptoms.frame.origin.y+_lbl_OrderByNumberOfSymptoms.frame.size.height;
    _collectionView_NumberOfSymptoms.frame = changeFrame_CollectionViewNumSympToms;
    
    CGRect changeFrame_lblTapRemedies = _lbl_TapOnRemediesForMoreInformation.frame;
    changeFrame_lblTapRemedies.origin.y    =_collectionView_NumberOfSymptoms.frame.origin.y+_collectionView_NumberOfSymptoms.frame.size.height+20;
    _lbl_TapOnRemediesForMoreInformation.frame = changeFrame_lblTapRemedies;
    

    
    _scrollV.contentSize       = CGSizeMake(50*(arrAllRemediesAscending.count+3), 1);
    _scrollV_Main.contentSize   = CGSizeMake(1, _collectionView_NumberOfSymptoms.frame.origin.y+_collectionView_NumberOfSymptoms.frame.size.height+200);
    
 
     [progressView hide:YES];
}
#pragma mark - TextViewDelegate

#pragma mark - Get View Information
-(void)DeleteTable_WhereTableName:(NSString *)TableName
{
    
    NSString *deleteQuary = [NSString stringWithFormat:@"DELETE FROM %@",TableName];
    BOOL success =  [app.database executeUpdate:deleteQuary];
       
}
-(void)getCheckboxSelected_Ids
{
    [app.arrCheckBoxClikedId removeAllObjects];
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id FROM tbl_shifa where selected ='1' order by Remedies"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *strTemp = [results stringForColumn:@"_id"];
        [app.arrCheckBoxClikedId addObject:strTemp];
    }
}

-(void)insertInto_tbl_report
{
       for (int i = 0; i<app.arrCheckBoxClikedId.count; i++) {
        
        BOOL isrecordPresent = [self IsRecordisPresentIn_tbl_report_WhereIdd:[app.arrCheckBoxClikedId objectAtIndex:i]];
        
        if(isrecordPresent==NO)
        {
            NSArray *arrFetched = [self getRecordsWhereId:[app.arrCheckBoxClikedId objectAtIndex:i]];
            NSString *str_id = [arrFetched objectAtIndex:0];
            NSString *str_MainCategory = [arrFetched objectAtIndex:1];
            NSString *str_FullName     = [arrFetched objectAtIndex:2];
            NSString *str_Remedies     = [arrFetched objectAtIndex:3];
         
            NSArray *arrSpliteRemedies = [str_Remedies componentsSeparatedByString:@":"];
            for(int j=0; j<arrSpliteRemedies.count-1; j++)
            {
                NSString *str_PerticularOneRemedies = [arrSpliteRemedies objectAtIndex:j];
                NSArray *arrSpliteRemediesIntencity = [str_PerticularOneRemedies componentsSeparatedByString:@","];
                NSString *str_PerticularRemedies = [arrSpliteRemediesIntencity objectAtIndex:0];
                NSString *str_PerticularIntencity   = [arrSpliteRemediesIntencity objectAtIndex:1];
                
                NSString *quary =[NSString stringWithFormat:@"Insert Into tbl_report (sync,_idd,maincategoy,Name,remedies,intensity) values (0,'%@','%@','%@','%@','%@')",str_id,str_MainCategory,str_FullName,str_PerticularRemedies,str_PerticularIntencity];
                BOOL success = [app.database executeUpdate:quary];
                
            }
        }
    }
    
}
-(BOOL)IsRecordisPresentIn_tbl_report_WhereIdd:(NSString *)_idd{

    BOOL isRecordExists = NO;
    NSString * queryStr=[NSString stringWithFormat:@"SELECT * FROM tbl_report WHERE _idd=%@",_idd];
    FMResultSet *results = [app.database executeQuery:queryStr];
    
    while([results next])
    {
        isRecordExists=YES;
    }
    return isRecordExists;
}

-(NSArray *)getRecordsWhereId:(NSString *)_id{
    
    NSArray *arrTemp;
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT * FROM tbl_shifa where _id ='%@' COLLATE NOCASE order by newrem",_id];
    
    
        FMResultSet *results = [app.database executeQuery:QuaryString];
        while([results next]) {

            NSString *tempId = [results stringForColumn:@"_id"];
            NSString *tempMaincategory = [results stringForColumn:@"maincategoy"];
            NSString *tempFullName = [results stringForColumn:@"newrem"];
            tempFullName = [tempFullName stringByReplacingOccurrencesOfString:@"|" withString:@","];
            NSString *tempRemedies = [results stringForColumn:@"Remedies"];
            
            arrTemp = [NSArray arrayWithObjects:tempId,tempMaincategory,tempFullName,tempRemedies, nil];
            
    }
    return arrTemp;
}
-(void)getAllTitlesFrom_tbl_report
{
   
    [arrAllTitles removeAllObjects];
    
    for (int jk=0; jk<app.arrCheckBoxClikedId.count; jk++) {
        
        NSString *QuaryString =[NSString stringWithFormat:@"SELECT * FROM tbl_report Where _idd=%@ order by Name",[app.arrCheckBoxClikedId objectAtIndex:jk]];
        FMResultSet *results = [app.database executeQuery:QuaryString];
        while([results next]) {
            
            NSString *tempName = [results stringForColumn:@"Name"];
            if(![arrAllTitles containsObject:tempName])
            {
                [arrAllTitles addObject:tempName];
            }
            
        }

    }

}
-(void)getAllRemediesInAscendingOrderForTopCollectionViewFrom_tbl_report
{
    [arrAllRemediesAscending removeAllObjects];
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT * FROM tbl_report order by remedies"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *tempRemedies = [results stringForColumn:@"remedies"];
        if(![arrAllRemediesAscending containsObject:tempRemedies])
        {
            [arrAllRemediesAscending addObject:tempRemedies];
        }
        
    }
 }
-(void)getAllRemediesForPutInto_BottomCollectionView
{
    [arrAllRemediesForBottomCollectionView removeAllObjects];
   
    
    for (int i=0; i<arrAllRemediesAscending.count; i++) {
        
        NSString *strPerticularRemedies = [arrAllRemediesAscending objectAtIndex:i];
        
        for (int jk=0; jk<arrAllTitles.count; jk++) {
            NSString *PerticularRemedisValue = @"";
            NSString *strPerticularlblName = [arrAllTitles objectAtIndex:jk];
            NSString *QuaryString =[NSString stringWithFormat:@"SELECT * FROM tbl_report where Name ='%@' AND remedies='%@' COLLATE NOCASE",strPerticularlblName,strPerticularRemedies];
        
            FMResultSet *results = [app.database executeQuery:QuaryString];
            while([results next]) {
            
                PerticularRemedisValue = [results stringForColumn:@"intensity"];
            }
             [arrAllRemediesForBottomCollectionView addObject:PerticularRemedisValue];
        }

    }
   
}

-(int)get_idd_ForRemoveMinusBtnTagWhereName:(NSString *)name
{
    NSString *strReturnString = @"";
    
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _idd FROM tbl_report where Name ='%@'",name];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        strReturnString = [results stringForColumn:@"_idd"];
       
    }
    
    return [strReturnString intValue];
}
#pragma mark-Order By sum Of Grading
-(void)get_BothArrayForSumOfGradingCollectionView
{
   
    [arr_RemediesSumName removeAllObjects];
     [arr_RemediesSumCount removeAllObjects];
    
    [arr_RemediesSumName addObject:@"Remedies"];
    [arr_RemediesSumCount addObject:@"Total"];
    
    NSString *QuaryString =[NSString stringWithFormat:@"select sum(intensity) cnt , remedies from tbl_report group by remedies order by cnt  desc"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
       NSString *strCount    = [results stringForColumn:@"cnt"];
       NSString *strRemedies = [results stringForColumn:@"remedies"];
        
        if (strCount.length>0 && strRemedies.length>0) {
            
            [arr_RemediesSumCount addObject:[NSString stringWithFormat:@"%@",strCount]];
            [arr_RemediesSumName addObject:[NSString stringWithFormat:@"%@",strRemedies]];
        }
        
    }
    
}
#pragma mark-Order By sum Of Grading
-(void)get_BothArrayForNumberOfSysmtemsCoveredCollectionView
{
   
    [arr_RemediesNumberOfSymtemsCoverdName removeAllObjects];
    [arr_RemediesNumberOfSymtemsCoverdCount removeAllObjects];
    
    [arr_RemediesNumberOfSymtemsCoverdName addObject:@"Remedies"];
    [arr_RemediesNumberOfSymtemsCoverdCount addObject:@"Coubt"];
    
    
    NSString *QuaryString =[NSString stringWithFormat:@"select count(intensity) cnt , remedies from tbl_report group by remedies order by cnt  desc"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *strCount    = [results stringForColumn:@"cnt"];
        NSString *strRemedies = [results stringForColumn:@"remedies"];
        
        if (strCount.length>0 && strRemedies.length>0) {
            
            [arr_RemediesNumberOfSymtemsCoverdCount addObject:[NSString stringWithFormat:@"%@",strCount]];
            [arr_RemediesNumberOfSymtemsCoverdName addObject:[NSString stringWithFormat:@"%@",strRemedies]];
        }
        
    }
  
}
#pragma mark - Draw View
-(void)drawSelectedCheckBox_lblName
{
   //Set 1Horizontal Remendies Title
    lbl_RemoveSymptom = [[UIBorderLabel alloc]init];
    lbl_RemoveSymptom.frame = CGRectMake(0, 0, 150, 40);
    lbl_RemoveSymptom.numberOfLines = 0;
    lbl_RemoveSymptom.leftInset = 5;
    lbl_RemoveSymptom.backgroundColor = [UIColor grayColor];
    lbl_RemoveSymptom.font = [UIFont systemFontOfSize:12];
    lbl_RemoveSymptom.text = @"Remove Symptom";
    lbl_RemoveSymptom.textColor = [UIColor whiteColor];
    lbl_RemoveSymptom.userInteractionEnabled = YES;
    [_scrollV addSubview:lbl_RemoveSymptom];
    
    
    
  //Set 2Horizontal Remedies Btn
    float y_OriginYtitles = lbl_RemoveSymptom.frame.origin.y+lbl_RemoveSymptom.frame.size.height;
    float lblheight = 40;
    float lblWidth  = lbl_RemoveSymptom.frame.size.width;
    for (int i = 0; i < arrAllTitles.count; i++) {

        UIBorderLabel*lbl_title = [[UIBorderLabel alloc]init];
        lbl_title.frame = CGRectMake(0, y_OriginYtitles, lblWidth, lblheight);
        lbl_title.numberOfLines = 0;
        lbl_title.leftInset = 37;
        lbl_title.font = [UIFont systemFontOfSize:12];
        lbl_title.text = [arrAllTitles objectAtIndex:i];
        lbl_title.backgroundColor = [UIColor whiteColor];
        lbl_title.userInteractionEnabled = YES;
        
        
        UIButton *btnToRemove = [UIButton buttonWithType:UIButtonTypeCustom];
        btnToRemove.frame = CGRectMake(0, 0, 35, lbl_title.frame.size.height-5);
        btnToRemove.tag = [self get_idd_ForRemoveMinusBtnTagWhereName:lbl_title.text];
        [btnToRemove addTarget:self action:@selector(RemoveCheckBoxClikedItem:) forControlEvents:UIControlEventTouchUpInside];
        [btnToRemove setBackgroundImage:[UIImage imageNamed:@"ic_cateogryminus.png"] forState:UIControlStateNormal];
        [lbl_title addSubview:btnToRemove];
        [_scrollV addSubview:lbl_title];
        
        y_OriginYtitles =lbl_title.frame.origin.y+lbl_title.frame.size.height;
  
    }
}
#pragma mark - Actions
- (IBAction)onClick_SaveAsBtn:(id)sender
{
    [self showHideSaveAsView];
}

- (IBAction)onClick_BackBtn:(id)sender {
    
    [self.navigationController popViewControllerAnimated:NO];
    
}
- (IBAction)onClike_Delete:(id)sender {
    
    NSString *Quary = [NSString stringWithFormat:@"UPDATE tbl_shifa SET selected ='0' WHERE  selected='1'"];
    BOOL isSuccess = [app.database executeUpdate:Quary];
     [app.arrCheckBoxClikedId removeAllObjects];
    
    if(isSuccess == YES && app.arrCheckBoxClikedId.count==0)
    {
        
        alert_DeletedAllSelectedItems = [[UIAlertView alloc]initWithTitle:@"Deleted all selected items" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:@"OK", nil];
        [alert_DeletedAllSelectedItems show];
        
    }
}
- (IBAction)onClick_BackBtn2:(id)sender {
    
    [self.navigationController popViewControllerAnimated:NO];

    
}
-(void)RemoveCheckBoxClikedItem:(id)sender
{
    UIButton *clikedBtn = (UIButton *)sender;

    for(UIView *view in _scrollV.subviews)
    {
        if(![view isKindOfClass:[UICollectionView class]] && ![view isKindOfClass:[UITableView class]]  )
        {
          [view removeFromSuperview];
        }
    }
    
    
    NSString *strBtnTag = [NSString stringWithFormat:@"%d",clikedBtn.tag];
    [app.arrCheckBoxClikedId removeObject:strBtnTag];
    NSString *Quary = [NSString stringWithFormat:@"UPDATE tbl_shifa SET selected ='0' WHERE _id =%@",strBtnTag];
    [app.database executeUpdate:Quary];
    
    NSString *Quary1 = [NSString stringWithFormat:@"delete from tbl_report  WHERE _idd =%@",strBtnTag];
    [app.database executeUpdate:Quary1];
    
     [progressView show:YES];
    [self performSelector:@selector(FavoriteScreen_Flow) withObject:nil afterDelay:0.1];
    
}

-(void)showHideSaveAsView
{
    if (!isSaveAsViewVisible)
    {
        
        [UIView animateWithDuration:0.5
                              delay:0.0
                            options:UIViewAnimationOptionCurveEaseIn
                         animations:^{
                             [saveAsView.view setAlpha:1.0];
                         }
                         completion:^(BOOL finished){
                         }
         ];
    }
    else
    {
        
        [UIView animateWithDuration:0.5
                              delay:0.0
                            options:UIViewAnimationOptionCurveEaseOut
                         animations:^{
                             [saveAsView.view setAlpha:0.0];
                         }
                         completion:^(BOOL finished){
                         }
         ];
    }
    
    isSaveAsViewVisible = !isSaveAsViewVisible;
    
}

#pragma mark - UITableView Delegate

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return arrAllRemediesAscending.count;
}
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath{
    static NSString *CellIdentifier = @"Cell";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil)
    {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault
                                       reuseIdentifier:CellIdentifier];
        cell.selectionStyle = UITableViewCellSelectionStyleGray;
        cell.textLabel.textAlignment = NSTextAlignmentCenter;
        cell.contentView.backgroundColor = [UIColor grayColor];
        cell.textLabel.transform = CGAffineTransformMakeRotation(M_PI/2);
       //cell.textLabel.layer.transform = CATransform3DRotate(CATransform3DIdentity,1.57079633,0,0,1);
    }
    

    
    //Add Under_line
    NSMutableAttributedString *attributeString = [[NSMutableAttributedString alloc] initWithString:[arrAllRemediesAscending objectAtIndex:indexPath.row]];
    UIFont *font=[UIFont fontWithName:@"Helvetica-Bold" size:13.0f];
    [attributeString addAttribute:NSFontAttributeName value:font range:NSMakeRange(0, [attributeString length])];
    [attributeString addAttribute:NSForegroundColorAttributeName value:[UIColor whiteColor] range:NSMakeRange(0, [attributeString length])];
    [attributeString addAttribute:NSUnderlineStyleAttributeName
                            value:[NSNumber numberWithInt:1]
                            range:(NSRange){0,[attributeString length]}];
    [attributeString addAttribute:NSUnderlineStyleAttributeName value:[NSNumber numberWithInt:1] range:(NSRange){0,[attributeString length]}];
    cell.textLabel.attributedText = attributeString;
    cell.contentView.backgroundColor = [UIColor grayColor];
    return cell;
}

-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    
    [tableView deselectRowAtIndexPath:indexPath animated:NO];
    
    app.strPassStringKentFavoriteToDetailRemedies = [arrAllRemediesAscending objectAtIndex:indexPath.row];

    DetailDescriptionOfRemedies *detailScreen = [self.storyboard instantiateViewControllerWithIdentifier:@"FavoriteToDetailRemedies"];
    [self.navigationController pushViewController:detailScreen animated:NO];
    
    

    
}


#pragma mark - UICollectionView Delegate
-(NSInteger)numberOfSectionsInCollectionView:(UICollectionView *)collectionView
{
    
    return 1;
    
}
-(NSInteger)collectionView:(UICollectionView *)collectionView numberOfItemsInSection:(NSInteger)section
{
    int intNumberOfRows=0;
    
    if(collectionView == _collectionview_Bottom)
    {
        intNumberOfRows = arrAllRemediesForBottomCollectionView.count;
    }
    if(collectionView == _collectionView_SumOfGrading)
    {
        intNumberOfRows = arr_RemediesSumName.count;
    }
    if(collectionView == _collectionView_NumberOfSymptoms)
    {
        intNumberOfRows = arr_RemediesNumberOfSymtemsCoverdName.count;
    }
   
    return intNumberOfRows;
}
-(UICollectionViewCell *)collectionView:(UICollectionView *)collectionView cellForItemAtIndexPath:(NSIndexPath *)indexPath
{
    UICollectionViewCell *customCell = nil;
    static NSString *CellIdentifier = @"Cell";
    
    if(collectionView == _collectionview_Bottom)
    {
     CellClass_BottomCollectionVIew *customCellBottom =[collectionView dequeueReusableCellWithReuseIdentifier:CellIdentifier forIndexPath:indexPath];
    //customCellBottom.lbl_BottomCollectionView.text = [NSString stringWithFormat:@"%li", (long)indexPath.row];
    customCellBottom.lbl_BottomCollectionView.text =[arrAllRemediesForBottomCollectionView objectAtIndex:indexPath.row];
    customCell = customCellBottom;
        
    }
    
   if(collectionView == _collectionView_SumOfGrading)
    {
        Cell_OrderBySumOfGradient *customCell_SumOfGradient =[collectionView dequeueReusableCellWithReuseIdentifier:CellIdentifier forIndexPath:indexPath];
        
        if(indexPath.row==0)
        {
            customCell_SumOfGradient.lbl_SubTitle.backgroundColor = [UIColor grayColor];
            customCell_SumOfGradient.lbl_SubTitle.textColor = [UIColor whiteColor];
            
            CGRect changeFrame1 = customCell_SumOfGradient.lbl_title.frame;
            changeFrame1.size.width = 100;
            customCell_SumOfGradient.lbl_title.frame = changeFrame1;
            
            CGRect changeFrame2 = customCell_SumOfGradient.lbl_SubTitle.frame;
            changeFrame2.size.width = 100;
            customCell_SumOfGradient.lbl_SubTitle.frame = changeFrame2;
            
        }
        else
        {
            customCell_SumOfGradient.lbl_SubTitle.backgroundColor = [UIColor colorWithRed:230.0/255.0 green:230.0/255.0 blue:164.0/255.0 alpha:1.0];
             customCell_SumOfGradient.lbl_SubTitle.textColor = [UIColor blackColor];
            
            CGRect changeFrame1 = customCell_SumOfGradient.lbl_title.frame;
            changeFrame1.size.width = 50;
            customCell_SumOfGradient.lbl_title.frame = changeFrame1;
            
            CGRect changeFrame2 = customCell_SumOfGradient.lbl_SubTitle.frame;
            changeFrame2.size.width = 50;
            customCell_SumOfGradient.lbl_SubTitle.frame = changeFrame2;
            
            
        }
        
        //Add Under_line
        NSMutableAttributedString *attributeString = [[NSMutableAttributedString alloc] initWithString:[arr_RemediesSumName objectAtIndex:indexPath.row]];
        UIFont *font=[UIFont fontWithName:@"Helvetica-Bold" size:13.0f];
        [attributeString addAttribute:NSFontAttributeName value:font range:NSMakeRange(0, [attributeString length])];
        [attributeString addAttribute:NSForegroundColorAttributeName value:[UIColor whiteColor] range:NSMakeRange(0, [attributeString length])];
        [attributeString addAttribute:NSUnderlineStyleAttributeName
                                value:[NSNumber numberWithInt:1]
                                range:(NSRange){0,[attributeString length]}];
        [attributeString addAttribute:NSUnderlineStyleAttributeName value:[NSNumber numberWithInt:1] range:(NSRange){0,[attributeString length]}];
        


       
        if(indexPath.row==0)
        {
            customCell_SumOfGradient.lbl_title.text = [arr_RemediesSumName objectAtIndex:indexPath.row];
        }else
        {
            customCell_SumOfGradient.lbl_title.attributedText = attributeString;
        }
        customCell_SumOfGradient.lbl_SubTitle.text =[NSString stringWithFormat:@"%@",[arr_RemediesSumCount objectAtIndex:indexPath.row]];
        
        customCell = customCell_SumOfGradient;
    
    }

    if(collectionView == _collectionView_NumberOfSymptoms)
    {
        
        Cell_OrderBySumOfGradient *customCell_NumberOfSymToms =[collectionView dequeueReusableCellWithReuseIdentifier:CellIdentifier forIndexPath:indexPath];
      
        if(indexPath.row==0)
        {
            customCell_NumberOfSymToms.lbl_SubTitle.backgroundColor = [UIColor grayColor];
             customCell_NumberOfSymToms.lbl_SubTitle.textColor = [UIColor whiteColor];
            CGRect changeFrame1 = customCell_NumberOfSymToms.lbl_title.frame;
            changeFrame1.size.width = 100;
            customCell_NumberOfSymToms.lbl_title.frame = changeFrame1;
            
            CGRect changeFrame2 = customCell_NumberOfSymToms.lbl_SubTitle.frame;
            changeFrame2.size.width = 100;
            customCell_NumberOfSymToms.lbl_SubTitle.frame = changeFrame2;
            
        }
        else
        {
            customCell_NumberOfSymToms.lbl_SubTitle.backgroundColor = [UIColor colorWithRed:230.0/255.0 green:230.0/255.0 blue:164.0/255.0 alpha:1.0];
            customCell_NumberOfSymToms.lbl_SubTitle.textColor = [UIColor blackColor];
            CGRect changeFrame1 = customCell_NumberOfSymToms.lbl_title.frame;
            changeFrame1.size.width = 50;
            customCell_NumberOfSymToms.lbl_title.frame = changeFrame1;
        
            CGRect changeFrame2 = customCell_NumberOfSymToms.lbl_SubTitle.frame;
            changeFrame2.size.width = 50;
            customCell_NumberOfSymToms.lbl_SubTitle.frame = changeFrame2;
            
  
        }
        
        
        //Add Under_line
        NSMutableAttributedString *attributeString = [[NSMutableAttributedString alloc] initWithString:[arr_RemediesNumberOfSymtemsCoverdName objectAtIndex:indexPath.row]];
        UIFont *font=[UIFont fontWithName:@"Helvetica-Bold" size:13.0f];
        [attributeString addAttribute:NSFontAttributeName value:font range:NSMakeRange(0, [attributeString length])];
        [attributeString addAttribute:NSForegroundColorAttributeName value:[UIColor whiteColor] range:NSMakeRange(0, [attributeString length])];
        [attributeString addAttribute:NSUnderlineStyleAttributeName
                                value:[NSNumber numberWithInt:1]
                                range:(NSRange){0,[attributeString length]}];
        [attributeString addAttribute:NSUnderlineStyleAttributeName value:[NSNumber numberWithInt:1] range:(NSRange){0,[attributeString length]}];

        
       
        if(indexPath.row==0)
        {
            customCell_NumberOfSymToms.lbl_title.text = [arr_RemediesNumberOfSymtemsCoverdName objectAtIndex:indexPath.row];
        }else
        {
            customCell_NumberOfSymToms.lbl_title.attributedText =attributeString;
        }
        customCell_NumberOfSymToms.lbl_SubTitle.text =[arr_RemediesNumberOfSymtemsCoverdCount objectAtIndex:indexPath.row];
        
        customCell = customCell_NumberOfSymToms;
    
    }
    
    return customCell;
}
- (CGSize)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout*)collectionViewLayout sizeForItemAtIndexPath:(NSIndexPath *)indexPath{

    CGSize mElementSize;
    
 if(collectionView==_collectionView_SumOfGrading){
     if(indexPath.row==0)
     {
         mElementSize = CGSizeMake(100, 50);
     }
     else
     {
        mElementSize =CGSizeMake(50, 50);
     }
 }
 else if(collectionView==_collectionView_NumberOfSymptoms){
        if(indexPath.row==0)
        {
            mElementSize = CGSizeMake(100, 50);
        }
        else
        {
            mElementSize =CGSizeMake(50, 50);
        }
     
    }
 
 else if(collectionView==_collectionview_Bottom){
     mElementSize =CGSizeMake(50, 40);
 }
    
 return mElementSize;
    
}
- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout*)collectionViewLayout minimumInteritemSpacingForSectionAtIndex:(NSInteger)section {
    return 0.0;
}

- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout*)collectionViewLayout minimumLineSpacingForSectionAtIndex:(NSInteger)section {
    return 0.0;
}


- (UIEdgeInsets)collectionView:
(UICollectionView *)collectionView layout:(UICollectionViewLayout*)collectionViewLayout insetForSectionAtIndex:(NSInteger)section {
    //return UIEdgeInsetsMake(0,8,0,0);  // top, left, bottom, right
    return UIEdgeInsetsMake(0,0,0,0);  // top, left, bottom, right
}

-(void)collectionView:(UICollectionView *)collectionView didSelectItemAtIndexPath:(NSIndexPath *)indexPath
{
    
    if(collectionView == _collectionView_SumOfGrading)
    {

        app.strPassStringKentFavoriteToDetailRemedies = [arr_RemediesSumName objectAtIndex:indexPath.row];
      
        DetailDescriptionOfRemedies *detailScreen = [self.storyboard instantiateViewControllerWithIdentifier:@"FavoriteToDetailRemedies"];
        [self.navigationController pushViewController:detailScreen animated:NO];

        
    }
    
    if(collectionView == _collectionView_NumberOfSymptoms)
    {
        
        app.strPassStringKentFavoriteToDetailRemedies = [arr_RemediesNumberOfSymtemsCoverdName  objectAtIndex:indexPath.row];
        
        DetailDescriptionOfRemedies *detailScreen = [self.storyboard instantiateViewControllerWithIdentifier:@"FavoriteToDetailRemedies"];
        [self.navigationController pushViewController:detailScreen animated:NO];
        
        
    }
    
    
}

#pragma mark - Alert Delegate Methods
-(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    if(alertView ==alert_DeletedAllSelectedItems)
    {
        if (buttonIndex==0)
        {
        
          [self.navigationController popViewControllerAnimated:NO];
        }
        
    }
    
    
}

-(void)HideAlertCustomeMethod:(UIAlertView *)alertToBeHidden
{
    [alertToBeHidden dismissWithClickedButtonIndex:[alertToBeHidden cancelButtonIndex] animated:YES];
    
}



@end
