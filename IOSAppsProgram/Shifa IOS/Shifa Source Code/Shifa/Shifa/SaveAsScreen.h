//
//  SaveAsScreen.h
//  Shifa
//
//  Created by My Mac on 5/12/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "UIScrollView_CustomClass.h"


@protocol SaveAsScreenDelegate <NSObject>
-(void)showHideSaveAsView;

@end



@interface SaveAsScreen : UIViewController<UITextViewDelegate,UITextFieldDelegate,UIGestureRecognizerDelegate>{
    
    
    
   id <SaveAsScreenDelegate> SaveAsScreenDelegateObj;
    float floatHoldScrollViewContentOffSetPriviousY;
    
}


@property (weak, nonatomic) IBOutlet UIScrollView_CustomClass *scrollV;


@property (weak, nonatomic) IBOutlet UIImageView *img_ContactTview;
@property (weak, nonatomic) IBOutlet UIImageView *img_NameTview;
@property (weak, nonatomic) IBOutlet UIImageView *img_CommentTview;


@property (weak, nonatomic) IBOutlet UITextView *tview_Contact;
@property (weak, nonatomic) IBOutlet UITextView *tview_Name;
@property (weak, nonatomic) IBOutlet UITextView *tview_Comment;




- (IBAction)onClick_CancelBtn:(id)sender;
- (IBAction)onClick_OkBtn:(id)sender;

 @property (nonatomic, retain) id <SaveAsScreenDelegate> SaveAsScreenDelegateObj;




@end
