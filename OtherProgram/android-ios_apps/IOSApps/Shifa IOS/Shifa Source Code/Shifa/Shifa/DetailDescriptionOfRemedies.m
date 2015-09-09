//
//  DetailDescriptionOfRemedies.m
//  Shifa
//
//  Created by My Mac on 5/12/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "DetailDescriptionOfRemedies.h"

@interface DetailDescriptionOfRemedies ()

@end

@implementation DetailDescriptionOfRemedies

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

#pragma mark - View LifeCycle

- (void)viewDidLoad
{
    [super viewDidLoad];
	app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
    
    img_BtnNormal = [UIImage imageNamed:@"BtnNormal.png"];
    img_BtnHighlighted = [UIImage imageNamed:@"BtnHighlited.png"];
    
    str_BoerickeHTMLstring = @"";
    str_JkHTMLstring       = @"";
    str_AllenHTMLstring    = @"";
    [self getDetailScreenData];
    [self arrangeTopTreeBtn];
    
    NSString *strToLoadWebViewFirstTime;
    if(str_BoerickeHTMLstring.length>0)
    {
        strToLoadWebViewFirstTime = str_BoerickeHTMLstring;
        [_btn_Boericke setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
        
    }
    else if (str_JkHTMLstring.length>0)
    {
        strToLoadWebViewFirstTime = str_JkHTMLstring;
        [_btn_jkkent setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
        
    }else if(str_AllenHTMLstring.length>0)
    {
        strToLoadWebViewFirstTime = str_AllenHTMLstring;
        [_btn_allen setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
        
    }
    
    
    strToLoadWebViewFirstTime = [strToLoadWebViewFirstTime stringByReplacingOccurrencesOfString:@"\n" withString:@"<br/>"];
    
    str_BoerickeHTMLstring = [str_BoerickeHTMLstring stringByTrimmingCharactersInSet:[NSCharacterSet whitespaceAndNewlineCharacterSet]];
    [_webView_DetailDescription loadHTMLString:strToLoadWebViewFirstTime baseURL:[NSURL fileURLWithPath:[[NSBundle mainBundle] bundlePath]]];
    
    
    
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}



#pragma mark - GetData
-(void)getDetailScreenData{
    
    NSString *data = [NSString stringWithFormat:@"data%@",app.strUserSelectedLanguage];
    NSString *allen = [NSString stringWithFormat:@"allen%@",app.strUserSelectedLanguage];
    NSString *kent = [NSString stringWithFormat:@"kent%@",app.strUserSelectedLanguage];
    
    
    NSString *quaryString = [NSString stringWithFormat:@"SELECT _id,%@,%@,%@ FROM tbl_rem_info where upper(rem) = '%@' COLLATE NOCASE",data,kent,allen,app.strPassStringKentFavoriteToDetailRemedies];
    
    FMResultSet *results = [app.database executeQuery:quaryString];
    while([results next])
    {
        str_BoerickeHTMLstring = [results stringForColumn:data];
        str_JkHTMLstring       = [results stringForColumn:kent];
        str_AllenHTMLstring    = [results stringForColumn:allen];
        
    }
    
    
    
}
-(void)arrangeTopTreeBtn
{
    
    int BlankStringCount = 0;
    if(str_BoerickeHTMLstring.length<=0) BlankStringCount++;
    if(str_JkHTMLstring.length<=0)       BlankStringCount++;
    if (str_AllenHTMLstring.length<=0)   BlankStringCount ++;
    
    
    switch (BlankStringCount) {
        case 1:
        {
            if(str_BoerickeHTMLstring.length<=0)
            {
                _btn_Boericke.hidden = YES;
                _btn_jkkent.hidden   = NO;
                _btn_allen.hidden    = NO;
                
                _btn_jkkent.frame = CGRectMake(0, 0, (self.view.frame.size.width/2)-2, _btn_jkkent.frame.size.height);
                _btn_allen.frame = CGRectMake(self.view.frame.size.width/2, 0,self.view.frame.size.width/2, _btn_allen.frame.size.height);
                
            }
            if(str_JkHTMLstring.length<=0)
            {
                _btn_Boericke.hidden = NO;
                _btn_jkkent.hidden   = YES;
                _btn_allen.hidden    = NO;
                
                _btn_Boericke.frame = CGRectMake(0, 0, (self.view.frame.size.width/2)-2, _btn_Boericke.frame.size.height);
                _btn_allen.frame = CGRectMake(self.view.frame.size.width/2, 0, self.view.frame.size.width/2, _btn_allen.frame.size.height);
                
            }
            if(str_AllenHTMLstring.length<=0)
            {
                _btn_Boericke.hidden = NO;
                _btn_jkkent.hidden   = NO;
                _btn_allen.hidden    = YES;
                
                _btn_Boericke.frame = CGRectMake(0, 0, (self.view.frame.size.width/2)-2, _btn_Boericke.frame.size.height);
                _btn_jkkent.frame = CGRectMake(self.view.frame.size.width/2, 0,self.view.frame.size.width/2, _btn_jkkent.frame.size.height);
            }
            
        }break;
        case 2:
        {
            
            if(str_BoerickeHTMLstring.length<=0 && str_JkHTMLstring.length<=0 )
            {
                _btn_Boericke.hidden = YES;
                _btn_jkkent.hidden   = YES;
                _btn_allen.hidden    = NO;
                _btn_allen.frame = CGRectMake(0, 0, self.view.frame.size.width, _btn_allen.frame.size.height);
                
            }
            if(str_JkHTMLstring.length<=0 && str_AllenHTMLstring.length<=0)
            {
                _btn_Boericke.hidden = NO;
                _btn_jkkent.hidden   = YES;
                _btn_allen.hidden    = YES;
                _btn_Boericke.frame = CGRectMake(0, 0, self.view.frame.size.width, _btn_Boericke.frame.size.height);
                
                
            }
            if(str_AllenHTMLstring.length<=0 && str_BoerickeHTMLstring.length<=0)
            {
                _btn_Boericke.hidden = YES;
                _btn_jkkent.hidden   = NO;
                _btn_allen.hidden    = YES;
                _btn_jkkent.frame = CGRectMake(0, 0, self.view.frame.size.width , _btn_jkkent.frame.size.height);
            }
            
            
            
        }break;
            
        default:
            break;
    }
    
    
    
}

#pragma mark - Search in WebView

- (NSInteger)highlightAllOccurencesOfString:(NSString*)str
{
    
    NSString *path = [[NSBundle mainBundle] pathForResource:@"UIWebViewSearch" ofType:@"js"];
    NSString *jsCode = [NSString stringWithContentsOfFile:path encoding:NSUTF8StringEncoding error:nil];
    [_webView_DetailDescription stringByEvaluatingJavaScriptFromString:jsCode];
    
    NSString *startSearch = [NSString stringWithFormat:@"MyApp_HighlightAllOccurencesOfString('%@')",str];
    [_webView_DetailDescription stringByEvaluatingJavaScriptFromString:startSearch];
    
    NSString *result = [_webView_DetailDescription stringByEvaluatingJavaScriptFromString:@"MyApp_SearchResultCount"];
    return [result integerValue];
}

- (void)removeAllHighlights
{
    [_webView_DetailDescription stringByEvaluatingJavaScriptFromString:@"MyApp_RemoveAllHighlights()"];
}

- (void)searchBar:(UISearchBar *)searchBar textDidChange:(NSString *)searchText
{
    if([searchText length] < 2)
    {
        [self removeAllHighlights];
        
    }
    else
    {
        [self highlightAllOccurencesOfString:searchText];
    }
}

#pragma mark - Actions

- (IBAction)onClick_BackBtn:(id)sender {
    
    [self.navigationController popViewControllerAnimated:NO];
    
}

- (IBAction)onClick_BoerickeBtn:(id)sender {
    
    [_btn_Boericke setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
    [_btn_jkkent setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    [_btn_allen setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    
    if(str_BoerickeHTMLstring.length>0)
    {
        
        str_BoerickeHTMLstring = [str_BoerickeHTMLstring stringByReplacingOccurrencesOfString:@"\n" withString:@"<br/>"];
        [_webView_DetailDescription loadHTMLString:str_BoerickeHTMLstring baseURL:nil];
    }
    else
    {
        [_webView_DetailDescription loadHTMLString:@"" baseURL:nil];
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"NO DATA" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
        
    }
    
}

- (IBAction)onClick_JKkentBtn:(id)sender {
    
    [_btn_Boericke setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    [_btn_jkkent setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
    [_btn_allen setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    
    if(str_JkHTMLstring.length>0)
    {
        
        str_JkHTMLstring = [str_JkHTMLstring stringByReplacingOccurrencesOfString:@"\n" withString:@"<br/>"];
        [_webView_DetailDescription loadHTMLString:str_JkHTMLstring baseURL:nil];
    }
    else
    {
        [_webView_DetailDescription loadHTMLString:@"" baseURL:nil];
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"NO DATA" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
        
    }
    
}

- (IBAction)onClick_AllenBtn:(id)sender {
    
    [_btn_Boericke setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    [_btn_jkkent setBackgroundImage:img_BtnNormal forState:UIControlStateNormal];
    [_btn_allen setBackgroundImage:img_BtnHighlighted forState:UIControlStateNormal];
    
    
    if(str_AllenHTMLstring.length>0)
    {
        
        str_AllenHTMLstring = [str_AllenHTMLstring stringByReplacingOccurrencesOfString:@"\n" withString:@"<br/>"];
        [_webView_DetailDescription loadHTMLString:str_AllenHTMLstring baseURL:nil];
    }
    else
    {
        [_webView_DetailDescription loadHTMLString:@"" baseURL:nil];
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"NO DATA" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
        
    }
    
    
    
}

#pragma mark - Alert Delegate Methods
-(void)HideAlertCustomeMethod:(UIAlertView *)alertToBeHidden
{
    [alertToBeHidden dismissWithClickedButtonIndex:[alertToBeHidden cancelButtonIndex] animated:YES];
    
}

@end
