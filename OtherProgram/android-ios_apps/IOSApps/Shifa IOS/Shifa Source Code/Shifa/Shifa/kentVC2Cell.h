//
//  kentVC2Cell.h
//  Shifa
//
//  Created by My Mac on 4/21/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "MICheckBox.h"
#import "SearchObjectClass.h"

@interface kentVC2Cell : UITableViewCell




@property (weak, nonatomic) IBOutlet UIButton *btn_ImageView;
@property (weak, nonatomic) IBOutlet UILabel  *lbl_underBtn;
@property (weak, nonatomic) IBOutlet UILabel  *lbl_title;
@property (weak, nonatomic) IBOutlet UILabel  *lbl_IntencityValue;
@property (weak, nonatomic) IBOutlet UIButton *btn_CheckboxDisable;

@property (weak, nonatomic) IBOutlet UITextView *txtView_KentVc1;




@end
