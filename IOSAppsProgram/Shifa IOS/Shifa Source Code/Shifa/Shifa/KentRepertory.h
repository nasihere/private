//
//  KentRepertory.h
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"
@interface KentRepertory : UIViewController<UITableViewDataSource,UITableViewDelegate>
{
    AppDelegate    * app;
    NSMutableArray * arrKentList;

}
@property (strong, nonatomic) IBOutlet UITableView *tableViewKent;

- (IBAction)onClickBack:(id)sender;
- (IBAction)onClickSearch:(id)sender;
- (IBAction)onClickReport:(id)sender;
- (IBAction)onClickPadBtn:(id)sender;

@end
