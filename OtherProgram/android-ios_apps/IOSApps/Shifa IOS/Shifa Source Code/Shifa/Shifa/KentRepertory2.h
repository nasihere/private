//
//  KentRepertory2.h
//  Shifa
//
//  Created by My Mac on 4/21/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"
#import "MBProgressHUD.h"
#import "Reachability.h"

@interface KentRepertory2 : UIViewController<UITableViewDataSource,UITableViewDelegate,UISearchDisplayDelegate,UISearchBarDelegate>
{
    AppDelegate         * app;
    Reachability        * reachability;
    MBProgressHUD       * progressView;
    BOOL                isErrorShown;
    NSMutableArray      * arrRowId;
    NSMutableArray      * arrSublevel;
    NSMutableArray      * arrRowTitle;
    NSMutableArray      * arrRowSubTitle;
    NSMutableArray      * arrRowIntencity;
    NSMutableArray      * arrRowRemedies;
    NSString            * strPreviousQuaryCategoryString;
    NSMutableDictionary * dix_BtnTagRowHeight;
    

    NSArray             * arrSearchResults;
    NSMutableArray      * arrAllObjects;
    BOOL                isSearching;
    BOOL                isSearchByViewVisible;
    UITextView          *txt_ToCalculateHeight;
    
}
//@property (strong,nonatomic) NSString *strSelectedRowStringFromKentVC1;
@property (strong, nonatomic) IBOutlet UITableView *tableViewKentVC2;
@property (strong, nonatomic) IBOutlet UIView *view_SearchBy;

- (IBAction)onClick_SelfCategoriesBtn:(id)sender;
- (IBAction)onClick_SelfCategoriesNsubCategoriesBtn:(id)sender;





- (IBAction)onClickBackVC2:(id)sender;
- (IBAction)onClickSearchVC2:(id)sender;
- (IBAction)onClickOverViewVC2:(id)sender;
- (IBAction)onClickPadBtn:(id)sender;
@end
