//
//  MaterialMedica2.h
//  Shifa
//
//  Created by My Mac on 4/30/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"

@interface MaterialMedica2 : UIViewController<UITableViewDelegate,UITableViewDataSource>
{
    AppDelegate     *app;
    UIImage         *img_BooksOpen;
    NSMutableArray  *arr_materialVc2Title;
    NSMutableArray  *arr_materialVc2Subtitle;
    NSMutableArray  *arrAllNSObjectClass;
    NSArray         *arrSearchResults;
}

@property (nonatomic ,strong) IBOutlet UITableView *table_MaterialMedica2;
- (IBAction)onClick_BackBtn:(id)sender;
- (IBAction)onClick_SearchBtn:(id)sender;

@end
