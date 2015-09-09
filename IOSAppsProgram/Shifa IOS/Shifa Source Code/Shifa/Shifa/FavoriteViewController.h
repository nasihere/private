//
//  FavoriteViewController.h
//  Shifa
//
//  Created by My Mac on 4/29/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"
#import "UIBorderLabel.h"
#import "MBProgressHUD.h"
#import "SaveAsScreen.h"
#import <QuartzCore/QuartzCore.h>

@interface FavoriteViewController : UIViewController<UICollectionViewDelegate,UICollectionViewDataSource,UICollectionViewDelegateFlowLayout ,UIScrollViewDelegate,UIAlertViewDelegate,UITableViewDelegate,UITableViewDataSource,SaveAsScreenDelegate>
{
    
    AppDelegate         *app;
    MBProgressHUD       *progressView;
    UITableView         *table_horizontalAtTop;
    NSMutableArray      *arrAllTitles;
    NSMutableArray      *arrAllRemediesAscending;
    NSMutableArray      *arrAllRemediesForBottomCollectionView;
    UIAlertView         *alert_DeletedAllSelectedItems;
    
    UIBorderLabel       *lbl_RemoveSymptom;
    SaveAsScreen        *saveAsView;
    BOOL                isSaveAsViewVisible;
    
    //Order By SumOfGradient
    NSMutableArray      *arr_RemediesSumCount;
    NSMutableArray      *arr_RemediesSumName;
    
    //Order By NumberOfSymtemsCoverd
    NSMutableArray      *arr_RemediesNumberOfSymtemsCoverdCount;
    NSMutableArray      *arr_RemediesNumberOfSymtemsCoverdName;
    
}


@property (weak, nonatomic) IBOutlet UIScrollView *scrollV_Main;
@property (weak, nonatomic) IBOutlet UIScrollView *scrollV;
@property (weak, nonatomic) IBOutlet UICollectionView *collectionview_Bottom;

@property (weak, nonatomic) IBOutlet UILabel *lbl_OrderBySumOfGrading;
@property (weak, nonatomic) IBOutlet UICollectionView *collectionView_SumOfGrading;


@property (weak, nonatomic) IBOutlet UILabel *lbl_OrderByNumberOfSymptoms;
@property (weak, nonatomic) IBOutlet UICollectionView *collectionView_NumberOfSymptoms;

@property (weak, nonatomic) IBOutlet UILabel *lbl_TapOnRemediesForMoreInformation;

- (IBAction)onClick_SaveAsBtn:(id)sender;
- (IBAction)onClick_BackBtn:(id)sender;
- (IBAction)onClike_Delete:(id)sender;
- (IBAction)onClick_BackBtn2:(id)sender;
-(void)RemoveCheckBoxClikedItem:(id)sender;

@end
