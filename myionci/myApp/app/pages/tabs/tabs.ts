import {Page, Tabs, NavController, NavParams, ActionSheet, Modal, IonicApp} from 'ionic-angular';
// Import all of the tab pages
import {TabHomePage} from '../tab-home/tab-home';
import {TabChallengesPage} from '../tab-challenges/tab-challenges';
import {TabPeoplePage} from '../tab-people/tab-people';
import {TabRewardsPage} from '../tab-rewards/tab-rewards';
import {TabMoreModal} from '../tab-more/tab-more';
// Import the more shared page
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/tabs/tabs.html',
  providers: [MoreShared]
})
export class TabsPage {
  // Declare the variables
  public tab1Root: any;
  public tab2Root: any;
  public tab3Root: any;
  public tab4Root: any;
  public tab5Root: any;
  public moreshared: any;
  // Declare the tab badges
  public homebadges: string = "";
  public challengebadges: string = "";
  public peoplebadges: string = "";
  public rewardbadges: string = "";
  public morebadges: string = "";

  public moretabposition: number = 4;         // More tab position

  constructor(private nav: NavController, moreshared: MoreShared, public app: IonicApp) {
    // This tells the tabs component which Pages
    // should be each tab's root Page
    this.tab1Root = TabHomePage;
    this.tab2Root = TabChallengesPage;
    this.tab3Root = TabPeoplePage;
    this.tab4Root = TabRewardsPage;
    this.tab5Root = TabMoreModal;
    this.moreshared = moreshared;
  }

  // Show more modal when More tab is clicked
  showModal() {
    // Create and show the more modal
    let modal = Modal.create(TabMoreModal);
    this.nav.present(modal);
    // Set the More modal background/hilite
    this.hiliteMoreTab();
  }

  // Hilight the More tab
  hiliteMoreTab() {
    document.getElementById('navtabs')
      .getElementsByTagName('tabbar')[0]
      .getElementsByTagName('a')[this.moretabposition]
      .setAttribute('aria-selected','true');
  }
}
