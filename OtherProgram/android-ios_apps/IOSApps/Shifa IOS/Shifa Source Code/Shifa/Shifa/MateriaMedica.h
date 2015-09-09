//
//  MateriaMedica.h
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"

@interface MateriaMedica : UIViewController<UITableViewDelegate,UITableViewDataSource,UIActionSheetDelegate>{
    
    AppDelegate      *app;
    NSMutableArray   *arr_MaterialVc1Title;
    NSMutableArray   *arr_MaterialVc1Subtitle;
    UIImage          *img_BooksOpen;
    NSMutableArray   *arrAllNSObjectClass;
    NSArray          *arrSearchResults;
    
    
    UIActionSheet    *actionSheet_language;
}
@property (weak, nonatomic) IBOutlet UITableView *table_MaterialMedika;


- (IBAction)onClickBack:(id)sender;
- (IBAction)onclick_Setting:(id)sender;
- (IBAction)onClick_SearchBtn:(id)sender;



@end
