import {NavController, Modal} from 'ionic-angular';
import {Injectable} from 'angular2/core';
// Import Pages to use
import {TosPage} from '../tos/tos';
import {PrivacyPolicyPage} from '../privacypolicy/privacypolicy';
import {RewardsTermsPage} from '../rewardsterms/rewardsterms';
import {AboutUsPage} from '../aboutus/aboutus';
import {ContactUsPage} from '../contactus/contactus';
import {FAQPage} from '../faq/faq';
import {SafetyTermsPage} from '../safetyterms/safetyterms';

@Injectable()
export class MoreShared {
    constructor(private nav: NavController) {
    }

    // Handle opening the TOS modal
    openFAQ() {
        let modal = Modal.create(FAQPage);
        this.nav.present(modal);
    }

    // Handle opening the TOS modal
    openTOS() {
        let modal = Modal.create(TosPage);
        this.nav.present(modal);
    }

    // Handle opening the Safety Terms modal
    openSafetyTerms() {
        let modal = Modal.create(SafetyTermsPage);
        this.nav.present(modal);
    }

    // Handle opening the Privacy modal
    openPrivacy() {
        let modal = Modal.create(PrivacyPolicyPage);
        this.nav.present(modal);
    }

    // Handle opening the Rewards Terms modal
    openRewardsTerms() {
        let modal = Modal.create(RewardsTermsPage);
        this.nav.present(modal);
    }

    // Handle opening the About Us modal
    openAboutUs() {
        let modal = Modal.create(AboutUsPage);
        this.nav.present(modal);
    }

    // Handle opening the Contact Us modal
    openContactUs() {
        let modal = Modal.create(AboutUsPage);
        this.nav.present(modal);
    }
}
