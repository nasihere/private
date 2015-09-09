//
//  MaterialMedica2.m
//  Shifa
//
//  Created by My Mac on 4/30/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "MaterialMedica2.h"
#import "DetailDescriptionScreen.h"
#import "NSObjectMedica2.h"

@interface MaterialMedica2 ()
@end

@implementation MaterialMedica2


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}


#pragma mark - View Lifecycle

- (void)viewDidLoad
{
    [super viewDidLoad];
	app = (AppDelegate *)[[UIApplication sharedApplication]delegate];
     img_BooksOpen = [UIImage imageNamed:@"home_materiamedica.png"];
    
    
    //Make Seprator line full
    if ([_table_MaterialMedica2 respondsToSelector:@selector(setSeparatorInset:)]) {
        [_table_MaterialMedica2 setSeparatorInset:UIEdgeInsetsZero];
    }
    [self getMaterialMedicaListVc2Data];
    _table_MaterialMedica2.contentOffset = CGPointMake(0, self.searchDisplayController.searchBar.frame.size.height+2);
    

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
-(void)getMaterialMedicaListVc2Data
{
    arrAllNSObjectClass      =[[NSMutableArray alloc]init];
    arr_materialVc2Title     =[[NSMutableArray alloc]init];
    arr_materialVc2Subtitle  =[[NSMutableArray alloc]init];
    arrSearchResults         =[[NSArray alloc]init];
  
    
    app.strSelectedRowStringAtMaterialMedicaVc1 = [app.strSelectedRowStringAtMaterialMedicaVc1 stringByAppendingString:@"%"];
    
    NSString *quaryString = [NSString stringWithFormat:@"SELECT _id,rem,RemediesName FROM tbl_rem_info where (data != '' or allen != '' or kent != '') and rem like '%@'COLLATE NOCASE order by rem",app.strSelectedRowStringAtMaterialMedicaVc1];
    FMResultSet *results = [app.database executeQuery:quaryString];
    while([results next])
    {
        NSObjectMedica2 *obj  = [NSObjectMedica2 new];
        NSString *title = [results stringForColumn:@"RemediesName"];
        if(title.length>0)
        {
            [arr_materialVc2Title addObject:title];
            obj.str_Title =title ;
        }
        
        
        NSString *subtitle = [results stringForColumn:@"rem"];
        if(subtitle.length>0)
        {
            [arr_materialVc2Subtitle addObject:subtitle];
            obj.str_Subtitle = subtitle;
        }
        
        [arrAllNSObjectClass addObject:obj];
    }
    
    
   [_table_MaterialMedica2 reloadData];
    
    
}




#pragma mark - Actions

- (IBAction)onClick_BackBtn:(id)sender {
    
     [self.navigationController popViewControllerAnimated:NO];
    
}

- (IBAction)onClick_SearchBtn:(id)sender {
    
   [_table_MaterialMedica2 scrollRectToVisible:CGRectMake(0, 0, 1, 1) animated:NO];
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
        _table_MaterialMedica2.hidden  = YES;
        tableView.frame           = _table_MaterialMedica2.frame;
        //tableView.frame         = CGRectMake(200, 0, 200, 400);
    }
    
}
-(void)searchDisplayController:(UISearchDisplayController *)controller didHideSearchResultsTableView:(UITableView *)tableView
{
    if(SYSTEM_VERSION_GREATER_THAN_OR_EQUAL_TO(@"7.0"))
    {
        _table_MaterialMedica2.hidden = NO;
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
        return arr_materialVc2Title.count;
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
    
    
    
    NSObjectMedica2 *Obj = nil;
    if (tableView == self.searchDisplayController.searchResultsTableView && arrSearchResults.count>0)
    {
        Obj = [arrSearchResults objectAtIndex:indexPath.row];
        
    } else
    {
        Obj = [arrAllNSObjectClass objectAtIndex:indexPath.row];
    }
    

    
    cell.imageView.image = img_BooksOpen;
    cell.imageView.contentMode = UIViewContentModeScaleAspectFit;
    cell.textLabel.text  = Obj.str_Title;
    cell.detailTextLabel.text = Obj.str_Subtitle;
    
    
    return cell;
}

-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    
    
    
    NSObjectMedica2 *NSObj = nil;
    _table_MaterialMedica2.hidden = NO;
    
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
        indexPath = [_table_MaterialMedica2 indexPathForSelectedRow];
        NSObj = [arrAllNSObjectClass objectAtIndex:indexPath.row];
        [_table_MaterialMedica2 deselectRowAtIndexPath:indexPath animated:NO];
    }
    
    
    
    

    
     app.strSelectedRowStringAtMaterialMedicaVc2 =NSObj.str_Subtitle;
    
    DetailDescriptionScreen *detailScreen = [self.storyboard instantiateViewControllerWithIdentifier:@"DetailDescriptionScreen"];
    [self.navigationController pushViewController:detailScreen animated:NO];
   
    
    
    
    
}



@end
