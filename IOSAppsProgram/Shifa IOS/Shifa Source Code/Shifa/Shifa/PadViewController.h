//
//  PadViewController.h
//  Shifa
//
//  Created by My Mac on 4/23/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"
#import "MBProgressHUD.h"

@interface PadViewController : UIViewController<UITableViewDelegate,UITableViewDataSource,UIAlertViewDelegate>{
    
    AppDelegate         * app;
    MBProgressHUD       * progressView;
    NSMutableArray      * arrRowIdPadVc;
    NSMutableArray      * arrRowSublevelPadVc;
    NSMutableArray      * arrRowTitlePadVc;
    NSMutableArray      * arrRowSubTitlePadVc;
    NSMutableArray      * arrRowIntencityPadVc;
    NSMutableArray      * arrRowRemediesPadVc;
    UITextView          * txt_ToCalculateHeight;
    UIAlertView         * alert_NoData;
    
    NSArray             * arrSearchResults;
    NSMutableArray      * arrAllObjects;
    BOOL                  isSearching;
        
}
//@property (strong,nonatomic) NSString *strSelectedRowStringFromKentVC1;
@property (strong, nonatomic) IBOutlet UITableView *tableViewKentPadVC;
@property (strong, nonatomic) NSString *strPassStringKentVC2toPadVC;

- (IBAction)onClickBackPadVC:(id)sender;
- (IBAction)onClickSearchPadVC:(id)sender;
- (IBAction)onClickOverViewPadVC:(id)sender;
- (IBAction)onClickPadBtnPadVC:(id)sender;

@end
