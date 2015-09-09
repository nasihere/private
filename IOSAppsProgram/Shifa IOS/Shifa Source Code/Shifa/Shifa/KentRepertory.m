//
//  KentRepertory.m
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "KentRepertory.h"
#import "KentRepertory2.h"
@interface KentRepertory ()

@end

@implementation KentRepertory

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

#pragma mark ----------------View Cycle-------------------
- (void)viewDidLoad
{
    [super viewDidLoad];
    app=[[UIApplication sharedApplication] delegate];
    
    //Make Seprator line full
    if ([_tableViewKent respondsToSelector:@selector(setSeparatorInset:)]) {
        [_tableViewKent setSeparatorInset:UIEdgeInsetsZero];
    }
    
       [self getKentRepertoryList];
    
    
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark ------------------Actions-------------------
-(void)getKentRepertoryList
{
    
        arrKentList=[[NSMutableArray alloc]init];
        FMResultSet *results = [app.database executeQuery:@"SELECT * FROM tbl_shifa where level='0' order by name"];
        while([results next]) {
            NSString *name = [results stringForColumn:@"name"];
            [arrKentList addObject:name];
        }
        [_tableViewKent reloadData];
        
    
    
}
-(BOOL)IsRecordisPresentIn_tblShifa_WhereCategory:(NSString *)category{
    
    BOOL isRecordExists = NO;
    NSMutableArray *arrTitleTemp = [[NSMutableArray alloc]init];
    
    
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id,Name,categoy,Intensity,Remedies,newrem,sublevel,selected FROM tbl_shifa where categoy ='%@' COLLATE NOCASE order by newrem",category];
  
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {
        
        NSString *tempTitle = [results stringForColumn:@"Name"];
        [arrTitleTemp addObject:tempTitle];
        
    }
    
    if(arrTitleTemp.count>0)
    {
        isRecordExists=YES;
    }
    else
    {
        
        UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Oops!!! Nothing inside..." message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
        [alert show];
        [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
        
    }
    return isRecordExists;
    
}

- (IBAction)onClickBack:(id)sender {
    
    [self.navigationController popViewControllerAnimated:YES];
}

- (IBAction)onClickSearch:(id)sender {
    
    
     UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Open any chapter to search" message:Nil delegate:self cancelButtonTitle:Nil otherButtonTitles:Nil, nil];
    [alert show];
    [self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];
    
}

- (IBAction)onClickReport:(id)sender {
}

- (IBAction)onClickPadBtn:(id)sender {
    
  
UIAlertView *alert = [[UIAlertView alloc]initWithTitle:@"Open any chapter to search" message:nil delegate:self cancelButtonTitle:nil otherButtonTitles:nil, nil];
[alert show];
[self performSelector:@selector(HideAlertCustomeMethod:) withObject:alert afterDelay:1.5];


}


#pragma mark ----------------Table View-------------------
-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return 1;
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return arrKentList.count;

}

-(UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    NSString * identifire=@"Cell";
    
    UITableViewCell * cell=[tableView dequeueReusableCellWithIdentifier:identifire];
    if(cell == nil)
    {
         cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:identifire];

    }
    cell.imageView.image=[UIImage imageNamed:@"icon-58.png"];
    cell.textLabel.text=[arrKentList objectAtIndex:indexPath.row];
    
    return cell;
}

-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
   
    BOOL isRecordPresent = NO;
    NSString *strPassString = [arrKentList objectAtIndex:indexPath.row];
    isRecordPresent = [self IsRecordisPresentIn_tblShifa_WhereCategory:strPassString];
    
    if(isRecordPresent == YES)
    {
   
   KentRepertory2 *kentVC2 = [self.storyboard instantiateViewControllerWithIdentifier:@"KentRepertory2"];
   [self.navigationController pushViewController:kentVC2 animated:NO];
    app.strSelectedRowStringAtKentVC1 = [arrKentList objectAtIndex:indexPath.row];
    
    }
    

}


#pragma mark - Alert Delegate Methods
-(void)HideAlertCustomeMethod:(UIAlertView *)alertToBeHidden
{
    [alertToBeHidden dismissWithClickedButtonIndex:[alertToBeHidden cancelButtonIndex] animated:YES];
    
}



@end
