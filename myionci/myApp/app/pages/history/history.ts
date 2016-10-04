import {Page, NavController, NavParams} from 'ionic-angular';
// Data providers
import {Historyfeed} from '../../providers/historyfeed/historyfeed';
// Components for the view
import {Historygifted} from '../../components/historygifted/historygifted';
import {Historylost} from '../../components/historylost/historylost';
import {Historypurchase} from '../../components/historypurchase/historypurchase';
import {Historywon} from '../../components/historywon/historywon';

@Page({
  templateUrl: 'build/pages/history/history.html',
  directives: [Historygifted, Historylost, Historypurchase, Historywon],
  providers: [Historyfeed]
})
export class HistoryPage {

  public allhistory: any;

  public feedinfo: {};

  constructor(private nav: NavController, private historyFeed: Historyfeed) {
    console.log("history page");

    this.allhistory = [];

    // Get the data for the Challenges Feed
    this.allhistory = historyFeed.loadResults();

  }
}