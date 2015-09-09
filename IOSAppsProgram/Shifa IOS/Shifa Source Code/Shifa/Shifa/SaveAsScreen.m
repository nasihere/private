//
//  SaveAsScreen.m
//  Shifa
//
//  Created by My Mac on 5/12/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "SaveAsScreen.h"

@interface SaveAsScreen ()

@end

@implementation SaveAsScreen
@synthesize SaveAsScreenDelegateObj;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
   
    UITapGestureRecognizer *tapRec = [[UITapGestureRecognizer alloc]
                                      initWithTarget:self action:@selector(tap:)];
    [self.view addGestureRecognizer: tapRec];
    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    
    
    
}
-(void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    floatHoldScrollViewContentOffSetPriviousY = 0.0;
    [_scrollV setContentOffset:CGPointMake(0, 0) animated:YES];
    _scrollV.contentSize = CGSizeMake([UIScreen mainScreen].bounds.size.width, [UIScreen mainScreen].bounds.size.height+200);

}

#pragma mark - Actions

-(void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event
{
     [_tview_Name resignFirstResponder];
     [_tview_Contact resignFirstResponder];
     [_tview_Comment resignFirstResponder];
    
}
-(void)tap:(UITapGestureRecognizer *)tapRec{
    
    [[self view] endEditing: YES];

     [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x, 0) animated:YES];
}
- (IBAction)onClick_CancelBtn:(id)sender {

    //Down Tview
    [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x, 0) animated:YES];

    [[self view] endEditing: YES];
     [self.SaveAsScreenDelegateObj showHideSaveAsView];
    
}
- (IBAction)onClick_OkBtn:(id)sender {
    
    if(_tview_Contact.text.length<=0)
    {

        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Please fill Contact Details" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
      
    }
    else if (_tview_Name.text.length <=0)
    {
        
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Please fill Name" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
        
        
    }
    else if (_tview_Comment.text.length <=0)
    {
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Please fill Comment" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
        
    }
    else
    {
       
         [[self view] endEditing: YES];
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@" You cannot use this section as guest. Please register or login as user.." message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
    }
    
}


#pragma mark - Tview Delegate
-(void)textViewDidBeginEditing:(UITextView *)textView
{
     //Up Tview
     floatHoldScrollViewContentOffSetPriviousY = _scrollV.contentOffset.y;

    
    if(textView == _tview_Name)
    {
        _img_NameTview.highlighted = YES;
        _tview_Name.textColor = [UIColor blackColor];
        
        [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x,_tview_Name.frame.origin.y-50) animated:YES];
        
    }
    if(textView == _tview_Contact)
    {
        _img_ContactTview.highlighted = YES;
        _tview_Contact .textColor = [UIColor blackColor];
        
        [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x,_tview_Contact.frame.origin.y-50) animated:YES];
        
    }
    if(textView == _tview_Comment)
    {
        _img_CommentTview.highlighted = YES;
        _tview_Comment.textColor = [UIColor blackColor];
        
        [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x,_tview_Comment.frame.origin.y-50) animated:YES];
        
        
    }

    
}
-(void)textViewDidEndEditing:(UITextView *)textView
{
    
    if(textView == _tview_Name)
    {
        _img_NameTview.highlighted = NO;
    }
    if(textView == _tview_Contact)
    {
        _img_ContactTview.highlighted = NO;
    }
    if(textView == _tview_Comment)
    {
        _img_CommentTview.highlighted = NO;
    }
    
    
    //Down Tview
    [_scrollV setContentOffset:CGPointMake(_scrollV.contentOffset.x, floatHoldScrollViewContentOffSetPriviousY) animated:YES];
    
    
}


#pragma mark - Alert Delegate Methods
-(void)HideAlertCustomeMethod:(UIAlertView *)alertToBeHidden
{
    [alertToBeHidden dismissWithClickedButtonIndex:[alertToBeHidden cancelButtonIndex] animated:YES];
    
}


@end
