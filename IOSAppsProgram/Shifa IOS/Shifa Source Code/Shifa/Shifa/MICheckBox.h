//
//  MICheckBox.h
//  aaa
//
//  Created by Appocraze on 7/5/13.
//  Copyright (c) 2013 Appocraze. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"



@interface MICheckBox : UIButton{
    
    AppDelegate *app;
    BOOL isChecked;
    BOOL isAppDelegate_sheredapplicationdelegate;
    int JumpTo_swichCase;
    

}

@property(nonatomic,assign)BOOL isChecked;
-(IBAction) checkBoxClicked:(id)tt;


@end
