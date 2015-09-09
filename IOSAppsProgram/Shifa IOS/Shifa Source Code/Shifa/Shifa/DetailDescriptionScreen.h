//
//  DetailDescriptionScreen.h
//  Shifa
//
//  Created by My Mac on 4/30/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AppDelegate.h"


@interface DetailDescriptionScreen : UIViewController<UIWebViewDelegate, UIScrollViewDelegate,UITextFieldDelegate,UISearchBarDelegate>
{
    
    AppDelegate *app;
    UIImage     *img_BtnNormal;
    UIImage     *img_BtnHighlighted;
    
    NSString    *str_BoerickeHTMLstring;
    NSString    *str_JkHTMLstring;
    NSString    *str_AllenHTMLstring;
    
}



@property (weak, nonatomic) IBOutlet UIButton *btn_Boericke;

@property (weak, nonatomic) IBOutlet UIButton *btn_jkkent;

@property (weak, nonatomic) IBOutlet UIButton *btn_allen;

@property (weak, nonatomic) IBOutlet UIWebView *webView_DetailDescription;


- (IBAction)onClick_BackBtn:(id)sender;

- (IBAction)onClick_BoerickeBtn:(id)sender;

- (IBAction)onClick_JKkentBtn:(id)sender;

- (IBAction)onClick_AllenBtn:(id)sender;


@end
