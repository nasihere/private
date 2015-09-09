//
//  MateriaMedica.m
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "MateriaMedica.h"
#import "MaterialMedica2.h"
#import "NSObjectMedica1.h"

@interface MateriaMedica ()

@end

@implementation MateriaMedica

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
    // Do any additional setup after loading the view.
    app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
    img_BooksOpen = [UIImage imageNamed:@"home_materiamedica.png"];
    [self getMaterialMedicaListData];
    _table_MaterialMedika.contentOffset = CGPointMake(0, self.searchDisplayController.searchBar.frame.size.height+2);
    

    
    //Make Seprator line full
    if ([_table_MaterialMedika respondsToSelector:@selector(setSeparatorInset:)]) {
        [_table_MaterialMedika setSeparatorInset:UIEdgeInsetsZero];
    }
    
       
}
-(void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    [self.searchDisplayController setActive:NO animated:NO];
    
}
- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

#pragma mark - Get View Data
-(void)getMaterialMedicaListData
{
    
    arr_MaterialVc1Title     =[[NSMutableArray alloc]init];
    arr_MaterialVc1Subtitle  =[[NSMutableArray alloc]init];
    arrAllNSObjectClass      =[[NSMutableArray alloc]init];
    arrSearchResults         =[[NSArray alloc]init];
    
    
    FMResultSet *results = [app.database executeQuery:@"SELECT _id,rem,RemediesName FROM tbl_rem_info where level = '0' order by rem"];
    while([results next]) {
        
        NSObjectMedica1 *obj  = [NSObjectMedica1 new];
        
        NSString *title = @"";
        title = [results stringForColumn:@"RemediesName"];
        [arr_MaterialVc1Title addObject:title];
        obj.str_Title = title;
        
        NSString *subtitle = @"";
        subtitle = [results stringForColumn:@"rem"];
        [arr_MaterialVc1Subtitle addObject:subtitle];
        obj.str_Subtitle = subtitle;
        
        [arrAllNSObjectClass addObject:obj];
        
    }
    
    [_table_MaterialMedika reloadData];
    
 
}

#pragma mark - Actions
- (IBAction)onClickBack:(id)sender {
    
    [self.navigationController popViewControllerAnimated:YES];
}

- (IBAction)onclick_Setting:(id)sender {
    
    actionSheet_language = [[UIActionSheet alloc]initWithTitle:@"Select language" delegate:self cancelButtonTitle:nil destructiveButtonTitle:@"Cancel" otherButtonTitles:@"English",@"Italian",@"Dutch",@"German",@"French",@"Spanish",@"Portuguese",nil];
    [actionSheet_language showInView:self.view];
    
}

- (IBAction)onClick_SearchBtn:(id)sender {
    
    [_table_MaterialMedika scrollRectToVisible:CGRectMake(0, 0, 1, 1) animated:NO];
    [self.searchDisplayController.searchBar becomeFirstResponder];

}

#pragma mark - Search Delegate Methods

- (void)filterContentForSearchText:(NSString*)searchText scope:(NSString*)scope
{
    
    NSPredicate *resultPredicate = [NSPredicate predicateWithFormat:@"str_Title contains[c] %@", searchText];
    arrSearchResults = [arrAllNSObjectClass filteredArrayUsingPredicate:resultPredicate];
    
  
    /*NOTE_J := Basically, a predicate is an expression that returns a Boolean value (true or false). You specify the search criteria in the format of NSPredicate and use it to filter data in the array. As the search is on the str_title of recipe, we specify the predicate as “str_title contains[c] %@”. The “str_title” refers to the name property of the Recipe object. NSPredicate supports a wide range of filters including:
     
     BEGINSWITH
     ENDSWITH
     LIKE
     MATCHES
     CONTAINS
     
     Here we choose to use the “contains” filter. The operator “[c]” means the comparison is case-insensitive.
     */
    
}
-(BOOL)searchDisplayController:(UISearchDisplayController *)controller shouldReloadTableForSearchString:(NSString *)searchString
{
    [self filterContentForSearchText:searchString
                               scope:[[self.searchDisplayController.searchBar scopeButtonTitles]
                                      objectAtIndex:[self.searchDisplayController.searchBar
                                                     selectedScopeButtonIndex]]];
    
    return YES;
}

- (void)searchBarTextDidBeginEditing:(UISearchBar *)searchBar {
    
    
}
- (void)searchBarSearchButtonClicked:(UISearchBar *)searchBar {
    
    NSLog(@"Search Clicked");
    
}

- (void)searchBarCancelButtonClicked:(UISearchBar *)searchBar {
    
    
    DLog(@"Cancel Cliked");
    
}

-(void)searchDisplayController:(UISearchDisplayController *)controller didShowSearchResultsTableView:(UITableView *)tableView {
    
    
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        
        //Make Seprator line full
        if ([tableView respondsToSelector:@selector(setSeparatorInset:)]) {
            [tableView setSeparatorInset:UIEdgeInsetsZero];
        }
        
        tableView.backgroundColor = [UIColor whiteColor];
        _table_MaterialMedika.hidden  = YES;
        tableView.frame           = _table_MaterialMedika.frame;
        //tableView.frame         = CGRectMake(200, 0, 200, 400);
    }
    
}
-(void)searchDisplayController:(UISearchDisplayController *)controller didHideSearchResultsTableView:(UITableView *)tableView
{
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        _table_MaterialMedika.hidden = NO;
        tableView.hidden = YES;
    }
    
}

#pragma mark - TableView Delegate Methods

-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return 1;
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
   
    if (tableView == self.searchDisplayController.searchResultsTableView )
    {
        return arrSearchResults.count;
    }
    else
    {
        return arr_MaterialVc1Title.count;
    }

    
}

-(UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    static NSString * CellIdentifire=@"Cell";
    UITableViewCell  * cell= [tableView dequeueReusableCellWithIdentifier:CellIdentifire];
    
    
    if(cell == nil)
    {
        
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:CellIdentifire];
        
    }
    else
    {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:CellIdentifire];
        
    }
    
    
    NSObjectMedica1 *Obj = nil;
    if (tableView == self.searchDisplayController.searchResultsTableView && arrSearchResults.count>0)
    {
        Obj = [arrSearchResults objectAtIndex:indexPath.row];
        
    } else
    {
        Obj = [arrAllNSObjectClass objectAtIndex:indexPath.row];
    }
    
    
    cell.imageView.image = img_BooksOpen;
    cell.imageView.contentMode = UIViewContentModeScaleAspectFit;
    cell.textLabel.text  =Obj.str_Title;
    cell.detailTextLabel.text = Obj.str_Subtitle;
    
    return cell;
}

-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
   
    NSObjectMedica1 *NSObj = nil;
    _table_MaterialMedika.hidden = NO;
    
    if (self.searchDisplayController.active)
    {
        if(indexPath.row<arrSearchResults.count){
            indexPath = [self.searchDisplayController.searchResultsTableView indexPathForSelectedRow];
            NSObj = [arrSearchResults objectAtIndex:indexPath.row];
            [self.searchDisplayController.searchResultsTableView deselectRowAtIndexPath:indexPath animated:NO];
        }
        
    }
    else
    {
        indexPath = [_table_MaterialMedika indexPathForSelectedRow];
        NSObj = [arrAllNSObjectClass objectAtIndex:indexPath.row];
        [_table_MaterialMedika deselectRowAtIndexPath:indexPath animated:NO];
    }
    

    
    app.strSelectedRowStringAtMaterialMedicaVc1 = NSObj.str_Subtitle;
    
    MaterialMedica2 *materialMedica2 = [self.storyboard instantiateViewControllerWithIdentifier:@"MaterialMedica2"];
    [self.navigationController pushViewController:materialMedica2 animated:NO];
   

}



#pragma mark - ActionSheet Delegate Methods

- (void)actionSheet:(UIActionSheet *)actionSheet clickedButtonAtIndex:(NSInteger)buttonIndex {
    
   if(actionSheet==actionSheet_language) {
       
            switch (buttonIndex) {
                case 1:
                {
                    app.strUserSelectedLanguage = @"";
                }break;
                case 2:
                {
                    app.strUserSelectedLanguage = @"_Italian";
                }break;
                case 3:
                {
                    app.strUserSelectedLanguage = @"_dutch";
                }break;
                case 4:
                {
                    app.strUserSelectedLanguage = @"_german";
                }break;
                case 5:
                {
                    app.strUserSelectedLanguage = @"_french";
                }break;
                case 6:
                {
                    app.strUserSelectedLanguage = @"_spanish";
                }break;
                case 7:
                {
                    app.strUserSelectedLanguage = @"_portuguese";
                }break;
                default:
                {
                    app.strUserSelectedLanguage = @"";
                }
                    break;
            }
       
            
        }
    
}


@end
