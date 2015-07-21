// SAFE CONSOLE.LOG
debug = function(message) {
    if (window.console != undefined) {
        //console.log(message);
    }
};
var reName = new RegExp(/^[a-zA-Z]+$/);
var reEmail = new RegExp(/^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/);
var reAddress = new RegExp(/^[0-9a-zA-Z]+$/);
var rePostalZip = new RegExp(/^[0-9]+$/);
var reDate = new RegExp(/^(0?[1-9]|1[012])\/(0?[1-9]|[12][0-9]|3[01])\/(199\d|[2-9]\d{3})$/);
var reNumber = new RegExp(/^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/);
// setup global VAR for image paths per environment, this is the folder that CONTAINS the /img/ folder.
var imgPath = "_assets/"; // will always start & end with a '/'
// 
// DEFAULT IMG PATH FOR PROTOTYPE = /_assets/
// IMG PATH FOR DEV16 SERVER = /prototype/inreview/SVP2/_bs/_assets/
// IMG PATH FOR DEV SERVER = /mod/


var svpApp = {
    init: function() {
        // PLACE MAIN DOCUMENT.READY SCRIPTS HERE!

        //spf - 02/19/2014 - Smooth Scroll - this will scroll to the target in the anchor tag
        //      to use, simply add the 'scroll' to the anchor's class and the #target won't get appended to the URL which is ugly
        //$('[data-toggle="popover"]').popover('slow');
        $(".scroll").on("click", "a", function(e) {
            e.preventDefault();
            //calculate destination place
            var dest = 0;
            if ($(this.hash).offset().top > $(document).height() - $(window).height()) {
                dest = $(document).height() - $(window).height();
            } else {
                dest = $(this.hash).offset().top;
            }
            //go to destination
            $('html').animate({ scrollTop: dest }, 500, 'swing');
        });

        $(".scroll-top-of-tabs").click(function(e) {
            e.preventDefault();
            var dest = 0;
            var thisLink = $(this).attr('data-scrollto');
            if ($('#' + thisLink).offset().top > $(document).height() - $(window).height()) {
                dest = parseInt($(document).height() - $(window).height(), 10);
            } else {
                dest = parseInt($('#' + thisLink).offset().top, 10);
            }
            //go to destination
            setTimeout(function() {
                $('html, body').animate({ scrollTop: dest }, 500, 'swing');
            }, 10);

        });

        // IE detection
        svpApp.util.ieDetect();
        svpApp.util.expandAll();
        //  $("input.datemask").mask("99/99/9999", { placeholder: " " });
        //svpApp.util();
        // touch event detection specifically for IE10
        svpApp.util.touchDetection();
        svpApp.util.tinToggle.init();
        //			svpApp.util.expandToggle();
        svpApp.util.toolTips.init();
        svpApp.util.altAccordListener();
        svpApp.util.charCounter();
        svpApp.util.enterAddress();
        svpApp.util.verifyAddress();
        //svpApp.util.ssnMask();
        //   svpApp.util.dateMask();
        //    svpApp.util.MMDDYYYYMask();
        //  svpApp.util.zipMask();
        //    svpApp.util.emailMask();
        //       svpApp.util.phoneMask();
        /////       svpApp.util.employmentInfo();
        //       svpApp.util.custRecID();

        //svpApp.util.datePicker();
        //  svpApp.util.number9mask();
        //   svpApp.util.number10mask();
        // setup clicks for top squares
        if (svpApp.util.isQ1) {
            /*
                $('.navbar-nav li:not(smallboxlarge)').children('a').on('click', function(){
                    $('.navbar-nav li.smallboxlarge').removeClass('smallboxlarge', 400);
                    $('#dashboard .panel, body>.container, body>.subnavbar, body>footer').addClass('dimmed', 600);
                    $(this).parent('li').addClass('smallboxlarge', 400);
                    $(this).siblings('.smallbox-inner').children('a.smallboxclose').on('click', function(e){
                        $(this).parent().parent('li.smallboxlarge').removeClass('smallboxlarge', 600);
                        $('#dashboard .panel.dimmed, body > .container.dimmed, body>.subnavbar, body>footer').removeClass('dimmed', 400);
                        e.preventDefault();
                        return false;
                    });
                });
                */
        };

        // add custom focus highlighting for select box and text input field
        $('.select').on("focus", "select", function() {
            $(this).next("span").addClass('select-focused');
            $(this).addClass('select-focused');
        });
        $('.select').on("blur", "select", function() {
            $(this).next("span").removeClass('select-focused');
            $(this).removeClass('select-focused');
        });
        $('input').focus(function() {
            $(this).addClass('input-focused');
        });
        $('input').blur(function() {
            $(this).removeClass('input-focused');
        });

        // <textarea> (&& <input>s) COUNTer mechanism (just add 'data-countlimit' and '.countThis' class to use)
        $('.countthis').each(function(index) {
            if ($(this).next('span.charcount').length < 1) {
                $(this).parent().append('<span class="charcount" id="count' + index + '"></span>');
            }
            $(this).attr('id', 'texta' + index);
            var thisID = $(this).attr('id');
            var thatID = 'count' + index;
            var countLimit = parseInt($(this).attr('data-countlimit'), 10);
            $('#' + thisID).off('keyup');
            $('#' + thisID).on('keyup', function() {
                svpApp.util.updateCountdown(countLimit, thatID, thisID);
            });
            $('#' + thisID).off('change');
            $('#' + thisID).on('change', function() {
                svpApp.util.updateCountdown(countLimit, thatID, thisID);
            });
        });

        // init the tab-listener if you switched tabs!
        //$('ul.nav-tabs li:not(".active") a').on('click', function(){
        //	svpApp.tabs.init();
        //});


        // this corrects the 'active' state of the top/bottom tabs once clicked
        $('ul.nav-tabs li a').on('click', function() {
            setTimeout(function() {
                // cjm 4/23/14 - comment out call to tabReInit - not needed since product_selection doesn't have checkboxes anymore
                // svpApp.productSelection.tabReInit(thisPanel);
                //
                //spf - have to re-initalize scrollbars here when tab change to active!!!! fixed 02/18/2014
                // init custom scrollbars
                if (!(svpApp.util.isIE > 9) || !(svpApp.util.isMetro)) {
                    if ($('body#customer_record').length > 0) {
                        $('.scrollcontent').customScrollbar();
                    };
                };
                //spf - 2-19-2014 - bug in Surface ONLY on tabbed pages - need to manually hide then remove the Bootstrap Popover from the DOM
                //      when a popup is open then you move to another tab, didn't close the popup or remove it - this will fix that.
                //$('.popover').hide().remove();

            }, 1);

            svpApp.tabs.setTabs($(this));
        });

        // top-tile hover action
        /*		if (!svpApp.util.isQ1){
                    $('div.navbar-default .container ul.navbar-nav > li').not('#logout').hoverIntent({
                        over: function(){
                            //$(this).children('.smallbox-inner').show();
                            //$(this).addClass('smallboxlarge');
                            $(this).addClass('smallboxlarge', 400);
                            $('body > div.container div.panel').addClass('dimmed', 300);
                        },
                        out: function(){
                            $(this).removeClass('smallboxlarge', 400);
                            $('body > div.container div.panel').removeClass('dimmed', 300);
                        }
                    });
                };
        */
        svpApp.util.expandToggle();

        if ($('div.text-truncate').length > 0) {
            svpApp.util.textTruncateListener();
        }

        // show the red-arrow on validation errors on pageload (find class '.valerror')
        svpApp.util.buildValErrors();

        // for table.table-selectable
        if ($('table.table-selectable').length > 0) {
            svpApp.util.tableSelectable();
        }

        if ($('.searchTable').length > 0) {
            svpApp.util.searchTable();
        }


// execute other page-specific inits
        // home/dashboard only
        if ($('body#dashboard').length > 0) {
            svpApp.dashboard.init();
        }

        // customer_record only
        if ($('body#customer_record').length > 0) {
            svpApp.customerRecord.init();
        }

        // required_customer only
        if ($('body#required_customer').length > 0) {
            svpApp.requiredCustomer.init();
        }

        // product_selection only
        if ($('body#product_selection').length > 0) {
            svpApp.productSelection.init();
        }

        // product_acceptance only
        if ($('body#product_acceptance').length > 0) {
            svpApp.productAcceptance.init();
        }

        // product_fulfillment only
        if ($('body#product_fulfillment').length > 0) {
            svpApp.productFulfillment.init();
        }

        // signature capture only
        if ($('#signature-fillable').length > 0) {
            svpApp.util.captureSignature();
        }

        // account details only
        if ($('body#account_detail').length > 0) {
            svpApp.accountDetail.init();
        }

        // search results only
        if ($('body#search_results').length > 0) {
            svpApp.searchResults.init();
        }

        // customer snapshot only
        //if ($('body#customer_snapshot').length > 0) { svpApp.customerSnapshot.init(); }

        // prod_confirm (product and services summary screens) only
        if ($('body#prod_confirm').length > 0) {
            svpApp.prodConfirm.init();
        }

        if ($('body.bootstrap-modal').length > 0) {
            svpApp.bootstrapModal();
        } else {
            svpApp.modal.init();
        };

        svpApp.util.buildSelects();
        svpApp.util.getDate();
        svpApp.tabs.init();
        svpApp.bootstrapModal();
        //svpApp.util.consoleToEl();

        // check for Q1 specific body class ('q1')
        if ($('body').hasClass('q1')) {
            svpApp.util.isQ1 = true;
        }
        //spf - 04/16/2014 - new enable/disable SVP Classic Tile
        if (typeof $("#classicsvp").attr('data-classic') !== "undefined") {
            var thisLink = $("#classicsvp").attr('data-classic');
            if (thisLink === 'off') {
                $("#classicsvp > a").click(function(e) {
                    e.preventDefault();
                    return false;
                }).css('cursor', 'default').addClass('disabled');
            }
        }


        $('a, .btn, .btn-search').on('click', function(e) {
            if ($(this).hasClass('disabled')) {
                return false;
            } else {
                var thisPleaseWait = $(this).attr('data-pleasewait');
                if (typeof thisPleaseWait !== 'undefined' && thisPleaseWait === 'true') {
                    $('body').append('<div class="blackout" id="waitoverlay" tabindex="-1" role="dialog" data-keyboard="false" data-backdrop="static">' +
                        '<div class="pleaseWait-centered">' +
                        '<div class="spinner dashes twelve wf-please-wait-spinner">' +
                        '<div></div><div></div><div></div><div></div><div></div><div></div>' +
                        '<div></div><div></div><div></div><div></div><div></div><div></div>' +
                        '</div>' +
                        '<div id="pleaseWaitText">Please Wait<span id="dots">...</span></div></div></div>'
                    );

                    var thisGoTo = $(this).attr('href');

                    //spf - 05/22/2014 - animated dots at the end of 'Please Wait'
                    $('#dots').html('');
                    var dots = 0;
                    setInterval(type, 300);

                    function type() {
                        if (dots < 3) {
                            $('#dots').append('.');
                            dots++;
                        } else {
                            $('#dots').html('');
                            dots = 0;
                        };
                    };

/* 
                      var thisWaitTime = $(this).attr('data-waittime');
                      if(typeof thisWaitTime !== 'undefined') {
                          thisWaitTime = parseInt(thisWaitTime);
                          if(isNaN(thisWaitTime)){
                              setTimeout(function(){window.location.href=thisGoTo;},3000);
                          } else {
                              if(thisWaitTime <=1000) {
                                  if(thisGoTo) {
                                      setTimeout(function(){window.location.href=thisGoTo;},thisWaitTime*3);
                                  } else {
                                      setTimeout(function(){
                                          $('.blackout').remove();
                                        }, thisWaitTime*3);
                                  };
                              } else {
                                  if(thisGoTo) {
                                      setTimeout(function(){window.location.href=thisGoTo;},thisWaitTime);
                                  } else {
                                      setTimeout(function(){
                                          $('.blackout').remove();
                                        }, thisWaitTime);
                                  };
                              };
                          };
                      } else {
                          if(thisGoTo) { // this is the default call for ET/Production, no timeout, simply wait until the next page loads
                              //setTimeout(function(){window.location.href=thisGoTo;},3000);
                          } else {
                              setTimeout(function(){
                                  $('.blackout').remove();
                                }, 3000);
                          };
                      };//end if data-waittime="xxxx"
                      */
                }; //end if data-pleaseWait="true"
            }; //end else hasClass disabled
        });
    }, // end svpApp.init()
    bootstrapModal: function() {

        /* SPF - http://stackoverflow.com/questions/12449890/reload-content-in-modal-twitter-bootstrap exaple 
         * should try this method and see if it's better
         */
        /*
        $("[data-toggle=modal]").click(function(ev) {
            ev.preventDefault();
            // load the url and show modal on success
            $($(this).attr('data-target') + " .modal-body").load($(this).attr("href"), function() {
                $($(this).attr('data-target')).modal("show");
            });
        });
        */

        $('a.openModal').on('click', function(e) {
            thisPanel = $(this).attr('data-remote');
            //thisPanel = $(this).attr('href');
            title = $(this).attr('data-title');
            cancelBtn = $(this).attr('data-cancelbtn');
            buttonsays = $(this).attr('data-buttontext');
            checkButtons();

        });

        function checkButtons() {
            if (typeof title == 'undefined') {
                title = 'Edit ' + thisPanel;
            };

            if (typeof buttonsays == 'undefined') {
                if (typeof cancelBtn == 'undefined') {
                    buttonsays = 'Save';
                } else {
                    buttonsays = 'Close';
                    cancelBtn = false;
                };
            };

        };

        function addModalContent() {
            //manually remove the please Wait if it is shown
            if ($('#waitoverlay')) {
                $('#waitoverlay').remove();
            };
            $('div.modalcontainer .modal-internal h2 span').empty();
            $('.modalcontainer .modal-internal h2 span').append(title);
            $('.modalcontainer .modalfooter').remove();
            if (buttonsays.toUpperCase() == 'SEARCH AGAIN') {
                $('.modalcontainer').append('<p class="modalfooter"><a class="modal-close">Close</a> <a class="btn btn-primary search-results" data-buttontext="SEARCH AGAIN" data-remote="modal_search.html" data-title="CUSTOMER SEARCH"><span>Apply</span></a></p>');
            } else {
                $('.modalcontainer').append('<p class="modalfooter"><a class="modal-close">Close</a> <a class="btn btn-primary" href="javascript:;"><span>Apply</span></a></p>');
            };
            //$('.modalcontainer').append('<div class="modal-footer no-border modalfooter"><a class="modal-close btn btn-primary" href="javascript:;">CLOSE</a></div>');
            $('.modalcontainer p.modalfooter a.btn span').empty().html(buttonsays);
            $('.modalcontainer > p.alignright a.btn-primary').empty().html(buttonsays);
            if (typeof cancelBtn !== 'undefined' || (cancelBtn)) {
                $('.modalcontainer p.modalfooter a.modal-close').remove();
            };
            /*
            if(buttonsays.toUpperCase() =='SEARCH AGAIN') {
                $('.modalcontainer p.modalfooter a.btn').addClass('search-results').attr('data-remote', 'modal_search.html').attr('data-title', 'CUSTOMER SEARCH').attr('data-cancelbtn', false).attr('data-buttontext','SEARCH AGAIN');
            };
            */

            return true;

        };

        function closeModal() {
            $('a.modal-close, p.modalfooter a.btn-primary').on('click', function(e) {
                $('#svpModal').modal('hide');
            });
        };

        $('#svpModal').on('shown.bs.modal', function(e) {
            addModalContent();
            expandCollapseAll();
            svpApp.util.buildSelects();
            closeModal();

            $('.search-results').on('click', function(e) {
                /*
                  if(!buttonsays){
                      // made this selector a global instead of 'search-results class as the SEARCH AGAIN button is being created after the 
                      // function call and can't set a class to it that the selector recognizes after it's instanciated
                      // so check if buttonsays is not undefined first, then both search buttons should be working.
                      return false;
                  };
                  e.preventDefault();
               */
                if (buttonsays.toUpperCase() !== 'SEARCH AGAIN') {
                    thisPanel = $(this).attr('data-remote');
                    title = $(this).attr('data-title');
                    cancelBtn = false;
                    buttonsays = $(this).attr('data-buttontext');
                } else {
                    alert('search again clicked');
                    thisPanel = 'modal_search.html';
                    title = 'CUSTOMER SEARCH';
                    cancelBtn = false;
                    buttonsays = 'CANCEL';
                };
                $('#dashboard_modal').empty();
                checkButtons();
                addModalContent();
                /*
                $.ajax({
        url : thisPanel,
        cache : true,
        //data : {custType: custType},
        success : function(data) {
                            $('#dashboard_modal').html(data);
        },
        timeout: 30000,
        error: function( jqXHR, textStatus, errorThrown) {
            var data = "<div class='col col-lg-6 col-md-6 col-sm-6 col-xs-8 col-lg-offset-3 col-md-offset-3 col-sm-offset-3 col-xs-offset-2 alert alert-information'>";
            data += "<p><strong>ERROR:</strong></p>";
            if (errorThrown.length == 0) {
                data += textStatus + " - Please Try Again";
            }
            else {
                data += errorThrown + " - Please Try Again";
            }
            data += "</div>";
                                
            //$('div.modalcontainer .modal-internal div#fillthis').html(data);
                                $('#dashboard_modal').html(data);
        }
    });
                */

                $.ajax(thisPanel, function(data) {
                    $('#dashboard_modal').html(data);
                });
            });


        });
        $('#svpModal').on('hidden.bs.modal', function(e) {
            $(e.target).removeData('bs.modal');
        });


/* spf - Individual Parent Modals used as data-target attribute from launching link to activate modal window
         * now consolidated into one single re-usable Modal above - 'svpModal'
         */

        /*
                        $('#recModal').on('shown.bs.modal', function (e) {
                            addModalContent();
                            expandCollapseAll();
                            $('a.modal-close, p.modalfooter a.btn-primary').on('click', function(e){
                               $('.modalfooter').remove();
                               $('#recModal').modal('hide');
                            });
                        });
                        $('#recModal').on('hidden.bs.modal', function (e) {
                            $(e.target).removeData('bs.modal');
                        });
        
                        
                        $('#snapModal').on('shown.bs.modal', function (e) {
                            addModalContent();
                            expandCollapseAll();
                            $('a.modal-close, p.modalfooter a.btn-primary').on('click', function(e){
                               $('.modalfooter').remove();
                               $('#snapModal').modal('hide');
                            });
                        });
                        $('#snapModal').on('hidden.bs.modal', function (e) {
                            $(e.target).removeData('bs.modal');
                        });
        
        
                        $('#searchModal').on('shown.bs.modal', function (e) {
                            addModalContent();
                            $('a.modal-close, p.modalfooter a.btn-primary').on('click', function(e){
                               $('.modalfooter').remove();
                               $('#searchModal').modal('hide');
                            });
                        });
                        $('#searchModal').on('hidden.bs.modal', function (e) {
                            $(e.target).removeData('bs.modal');
                        });
        */

        function expandCollapseAll() {

            /*
            $('a.expand-all').on('click', function(){
                if ($(this).hasClass('open')) {
                    $('a.accordToggle').not('.collapsed').trigger('click');
                    $(this).removeClass('open');
                    $(this).html('Expand All Sections');
                } else {
                    $('a.accordToggle.collapsed').trigger('click');
                    $(this).addClass('open');
                    $(this).html('Collapse All Sections');
                }
            });
              
            */
            /*  $('a.expand-collapse').on('click', function() {
                  if ($(this).text() == 'Collapse All Sections') {
                      $('a.accordToggle').not('.collapsed').trigger('click');
                      $(this).removeClass('open');
                      $(this).html('Expand All Sections');
                  } else {
                      $('a.accordToggle.collapsed').trigger('click');
                      $(this).addClass('open');
                      $(this).html('Collapse All Sections');
                  };
              });*/
        };

    },
    modal:
    {
        openmodal: function(href, datatitle, cancelbtn, buttontext) {
            var objPanel = href;
            var title = datatitle;
            var cancelBtn = cancelbtn;
            var buttonsays = buttontext;
            if (typeof title == 'undefined') {
                title = 'Edit ' + objPanel;
            }
            if (typeof buttonsays == 'undefined') {
                if (typeof cancelBtn == 'undefined') {
                    buttonsays = 'Save';
                } else {
                    buttonsays = 'Close';
                    cancelBtn = false;
                }
            }

            var testZero = objPanel.indexOf('#');
            if (testZero >= 0) {
                var data = $(objPanel).contents(); //var data = $('#'+objPanel).contents();
                svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
            } else {
                var data;
                svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
            }
            svpApp.modal.selector = objPanel;

            // e.preventDefault();


            return false;
        },

        opemm: function(obj) {
            var objPanel = $(obj).attr('href');
            var title = $(obj).attr('data-title');
            var cancelBtn = $(obj).attr('data-cancelbtn');
            var buttonsays = $(obj).attr('data-buttontext');
            if (typeof title == 'undefined') {
                title = 'Edit ' + objPanel;
            }
            if (typeof buttonsays == 'undefined') {
                if (typeof cancelBtn == 'undefined') {
                    buttonsays = 'Save';
                } else {
                    buttonsays = 'Close';
                    cancelBtn = false;
                }
            }

            var testZero = objPanel.indexOf('#');
            if (testZero >= 0) {
                var data = $(objPanel).contents(); //var data = $('#'+objPanel).contents();
                svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
            } else {
                var data;
                svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
            }
            svpApp.modal.selector = objPanel;

            e.preventDefault();


            return false;
        },
        init: function() {


            if ($('body > div.blackout').length < 1) {
                $('body').append('<div class="blackout" style="display:none;"></div><div class="modalcontainer clearfix" style="display:none"><p class="alignright marginbottom0 pull-right"><a class="modal-close" href="javascript:;"><strong>X</strong></a></p><div class="modal-internal">' +
                    '<h2><span></span></h2><div id="fillthis"></div></div><p class="modalfooter"><a class="modal-close">CANCEL</a> <a class="btn btn-primary" href="javascript:;"><span>Apply</span></a></p></div>');
            }
            $('a.editlink').on('click', function(e) {
                OpenModal(this);
            });
            $("input[type=radio].editlink").on("click", function(e) {
                OpenModal(this);
            });

            var OpenModal = function(obj) {
                var objPanel = $(obj).attr('data-target');
                var title = $(obj).attr('data-title');
                var cancelBtn = $(obj).attr('data-cancelbtn');
                var buttonsays = $(obj).attr('data-buttontext');
                //var customsize = $(obj).attr('data-style');
                if (typeof title == 'undefined') {
                    title = 'Edit ' + objPanel;
                }
                if (typeof buttonsays == 'undefined') {
                    if (typeof cancelBtn == 'undefined') {
                        buttonsays = 'Save';
                    } else {
                        buttonsays = 'Close';
                        cancelBtn = false;
                    }
                }

                var testZero = objPanel.indexOf('#');
                if (testZero >= 0) {
                    var data = $(objPanel).contents(); //var data = $('#'+objPanel).contents();
                    svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
                } else {
                    var data;
                    svpApp.modal.open(objPanel, title, data, buttonsays, cancelBtn);
                }
                svpApp.modal.selector = objPanel;
                //e.preventDefault();

                return false;
            };
            $('a.modal-close').on('click', function(e) {
                svpApp.modal.close(svpApp.modal.selector);
                e.preventDefault();
                return false;
            });

            // main 'blue button' click listener for modal windows
            $('p.modalfooter a.btn-primary').on('click', function(e) {
                // REPLACE WITH WHATEVER ACTIONS NEEDS TO HAPPEN ON BLUE BUTTON CLICK
                svpApp.modal.close(svpApp.modal.selector);
                e.preventDefault();
                return false;
            });

            $('select.select').focus(function() {
                $(this).next("span").addClass('select-focused');
                $(this).addClass('select-focused');
            });
        },
        selector: 'noSelector',
        open: function(selector, text1, text2, btntext, cancelBtn, customsize) {
            //selector=file name; text1=modal screen title; text2=content; btntext=button text; 
            //cancelBtn=true/false
            //console.log('in svpApp.modal.open, selector='+selector);
            $('div.blackout').fadeIn();
            $('div.modalcontainer .modal-internal h2 span').empty();
            var test = selector.indexOf('#');
            //var custType = 1;
            if (test >= 0) {
                svpApp.modal.selector = selector;
                $('div.modalcontainer').addClass(selector);
                $('div.modalcontainer .modal-internal div#fillthis').html(text2);
                //$('div.modalcontainer').removeAttr("style");

                var widthContainer = $($(selector)).width() + 20;
                var bodyContainer = $('body').width();

                $("div.modalcontainer").width(widthContainer);
                var LeftSpace = (bodyContainer - widthContainer) / 2;
                $("div.modalcontainer").css({ left: LeftSpace + "px" });
                svpApp.util.buildSelects();

            } else {
                $('div.modalcontainer .modal-internal div#fillthis').html('<img src="' + imgPath + 'img/ajax-loading.gif" id="loading" style="margin:40px auto;display:block;">');
                svpApp.modal.ajaxLoad(selector);
            }
            $('div.modalcontainer p.modalfooter a.btn span').empty().html(btntext);
            $('div.modalcontainer > p.alignright a.btn-primary').empty().html(btntext);
            $('div.modalcontainer .modal-internal h2 span').append(text1);
            $(window).scrollTop(0);
            $('div.modalcontainer').slideDown();
            if (typeof cancelBtn !== 'undefined' || (cancelBtn)) {
                $('.modalcontainer p.modalfooter a.modal-close').hide();
            }

            svpApp.bootstrapModal();
        },
        close: function(classname) {
            var oldContents = $('div.modalcontainer .modal-internal div#fillthis').contents();
            $('div.modalcontainer .modal-internal div#fillthis').empty();
            $(oldContents).appendTo(svpApp.modal.selector);
            $('div.modalcontainer').removeClass(classname);
            $('div.modalcontainer').slideUp(400);
            $('div.blackout').fadeOut(1200);
        },
        callback: function() {
            //spf - 02/17/2014 - try moving dataTable call and timeout out of each page and make it re-usable

            if ($('#acctHistory').length > 0) {
                setupDataTable('acctHistory');
            }
            if ($('#autoFinanceLoanHistory').length > 0) {
                setupDataTableLoans('autoFinanceLoanHistory');
            }
            if ($('#homeEquityLoanHistory').length > 0) {
                setupDataTableLoans('homeEquityLoanHistory');
            }
            if ($('#installmentHistory').length > 0) {
                setupDataTableLoans('installmentHistory');
            }
            if ($('#lineOfCreditHistory').length > 0) {
                setupDataTableLoans('lineOfCreditHistory');
            }
            if ($('#homeEquityLineHistory').length > 0) {
                setupDataTableLoans('homeEquityLineHistory');
            }
            if ($('#acctDetATMHistory').length > 0) {
                setupDataTableATM('acctDetATMHistory');
            }
            if ($('#atworkresults_modal').length > 0) {
                svpApp.util.tableSelectable_atWorkCode();
            }

            $('.dataTables_filter').find('input[type=text]').attr('maxlength', '20');

            function setupDataTableLoans(dataSelector) {
                var tableOne = $('#' + dataSelector).dataTable({
                    "aaSorting": [[0, 'desc']],
                    "aoColumns": [
                        null,
                        null,
                        { "sType": "currency", "asSorting": ["desc", "asc"] }
                    ]
                });

                setTimeout(function() {
                    tableOne.fnAdjustColumnSizing();
                }, 300);
            }

            function setupDataTableATM(dataSelector) {
                var tableOne = $('#' + dataSelector).dataTable({
                    "bSort": true,
                    "bAutoWidth": false,
                    "aaSorting": [[0, 'desc']],
                    "aoColumns": [
                        { "sType": "date", "asSorting": ["desc", "asc"] },
                        { "sType": "string", "asSorting": ["asc", "desc"] },
                        { "sType": "string", "asSorting": ["asc", "desc"] },
                        { "sType": "string", "asSorting": ["asc", "desc"] },
                        { "sType": "string", "asSorting": ["desc", "asc"] },
                        { "sType": "string", "asSorting": ["desc", "asc"] },
                        { "sType": "currency", "asSorting": ["desc", "asc"] }
                    ]
                });

                setTimeout(function() {
                    tableOne.fnAdjustColumnSizing();
                }, 300);
            }


            function setupDataTable(dataSelector) {
                // spf - NOTE:  This is specific to ALL Checking/Savings History Screens - with dataTables, if using 'aoColumns
                //       with sType it must match the number of fields in the table, which is currently 6
                //       - all other LOAN History screens are 3 so they can share a common function,
                //       except for Debit/ATM, which, again, is custom, so must have its own function

                // Custom sorting routines for dataTables are in plugins.js
                var tableOne = $('#' + dataSelector).dataTable({
                    "bSort": true,
                    "bAutoWidth": false,
                    "aaSorting": [[0, 'desc']],
                    "aoColumns": [
                        null,
                        null,
                        { "sType": "string", "asSorting": ["desc", "asc"] },
                        { "sType": "empty", "asSorting": ["desc", "asc"] },
                        { "sType": "currency", "asSorting": ["desc", "asc"] },
                        { "sType": "currency", "asSorting": ["desc", "asc"] }
                    ]
                });

                setTimeout(function() {
                    tableOne.fnAdjustColumnSizing();
                }, 300);

            }

            /*
            if (svpApp.util.isIE && svpApp.util.isTouch){ 

                var update_size = function() {
                $('.dataTable').css({ width: $('.dataTable').parent().width() });
                };

                $(window).resize(function() {
                  clearTimeout(window.refresh_size);
                  window.refresh_size = setTimeout(function() { update_size(); }, 250);
              });
               
            };
            */

            // for DOM-based functions that need to run AFTER the modal has opened
            $('.plus.phonePlus').on('click', function() {
                $(this).parent().parent().next('.hiddenphone').show();
                $(this).fadeOut();
                $(this).parent().parent().next('.hiddenphone').find('.plus.phonePlus2').css({ 'background-image': 'url(' + imgPath + 'img/plus.png)', 'cursor': 'default' });
            });

            var strContents = "";
            $(".hiddenphone #phone2").change(function() {
                strContents = $(this).val();
                if (strContents != "") {
                    $(".plus.phonePlus2")
                        .css({ 'background-image': 'url(' + imgPath + 'img/plus-active.png)', 'cursor': 'pointer' })
                        .on('click', function() {
                            $(this).parent().parent().parent().next('.hiddenphone').show();
                            $(this).fadeOut();
                        });
                }
            });

            // relationships panel qualify panel expand
            $('a.btn.qualifyclick').on('click', function(e) {
                $('#qualify_step1').slideUp();
                $('#qualify_step2').slideDown();
                e.preventDefault();
                return false;
            });
            $('select.select').focus(function() {
                $(this).next("span").addClass('select-focused');
                $(this).addClass('select-focused');
            });
            $('select.select').blur(function() {
                $(this).next("span").removeClass('select-focused');
                $(this).removeClass('select-focused');
            });
            $('input').focus(function() {
                $(this).addClass('input-focused');
            });
            $('input').blur(function() {
                $(this).removeClass('input-focused');
            });


            svpApp.util.buildSelects();
            svpApp.util.buildValErrors();
            svpApp.util.expandToggle();

            $('.phonemask').mask('(999) 999-9999', { placeholder: " " });
            $('input.ssnmask').mask('999-99-9999', { placeholder: " " });
            $("input.datemask").mask("99/99/9999", { placeholder: " " });

            // add any other modal scripts here!
            svpApp.util.charCounter('span#count1');
            //spf - 03/20/2014 - re-initializing toolTips breaks them - removing before it's discovered as a bug!
            //svpApp.util.toolTips.init();
            //spf - added inits to callback since we moved Recommendations and Snapshots from own html files to modal
            if ($('#customer_recommendations').length > 0) {
                svpApp.customerRecommendations.init();
            };
            if ($('#customer_snapshot').length > 0) {
                svpApp.customerSnapshot.init();
            };
            //spf - 04/22/2014 - need to initalize dashboard functions here after the modal_dashboard opens.
            if ($('#dashboard_modal').length > 0) {
                svpApp.dashboard.init();
            };
            if ($('#customer_record').length > 0) {
                svpApp.util.datePicker();
            };

        },
        ajaxLoad: function(url) {
            $.ajax({
                url: url,
                cache: true,
                //data : {custType: custType},
                success: function(data) {
                    $('img#loading').fadeOut(1200, function() {
                        $('div.modalcontainer .modal-internal div#fillthis').html(data);
                        svpApp.modal.callback();
                    });
                },
                timeout: 30000,
                error: function(jqXHR, textStatus, errorThrown) {
                    var data = "<div class='col col-lg-6 col-md-6 col-sm-6 col-xs-8 col-lg-offset-3 col-md-offset-3 col-sm-offset-3 col-xs-offset-2 alert alert-information'>";
                    data += "<p><strong>ERROR:</strong></p>";
                    if (errorThrown.length == 0) {
                        data += textStatus + " - Please Try Again";
                    } else {
                        data += errorThrown + " - Please Try Again";
                    }
                    data += "</div>";
                    $('div.modalcontainer .modal-internal div#fillthis').html(data);
                }
            });
        }
    },
    util: {
        ieDetect: function() {
            if (Function('/*@cc_on return document.documentMode===8@*/')()) {
                $("html").addClass("ie8");
                svpApp.util.isIE = 8;
            } else if (Function('/*@cc_on return document.documentMode===9@*/')()) {
                $("html").addClass("ie9");
                svpApp.util.isIE = 9;
            } else if (document.documentMode === 10) {
                $("html").addClass("ie10");
                svpApp.util.isIE = 10;
                svpApp.util.troubleshootTablet();

                function isActivexEnabled() {
                    var supported = null;
                    try {
                        // is in DESKTOP mode
                        supported = !!new ActiveXObject("htmlfile");
                    } catch (e) {
                        // is in METRO mode
                        supported = false;
                        $('body').addClass('ieMetro');
                        svpApp.util.isMetro = true;

                    }
                    return supported;
                }

                isActivexEnabled();
            } else {
                svpApp.util.isIE = false;
            }

            // now check for touch-specifically
            svpApp.util.touchDetection();

            return;
        },
        touchDetection: function() {
            // this is only designed for MS Surface touch events! - fires only once
            //$('body *').one('pointerdown', function(){
            if (navigator.maxTouchPoints) {
                svpApp.util.isTouch = true;
            };
        },
        //consoleToEl: function(){
        // needing to build a console.log for IE10(metro-mode && desktop-mode)
        //if (svpApp.util.isIE){
        //	if (!$('div#console').length){
        //		$('body').append('<div id="console"></div>');
        //	}
        //	window.console.log = function(msg){
        //		$('div#console').append(msg+'<br>');
        //	}
        // let's use Ctrl-C to toggle the console on the tablet!
        //	$.ctrl = function(key, callback, args) {
        //		var isCtrl = false;
        //		$(document).keydown(function(e) {
        //			if(!args) args=[];
        //			if(e.ctrlKey) isCtrl = true;
        //			if(e.keyCode == key.charCodeAt(0) && isCtrl) {
        //				callback.apply(this, args);
        //			}
        //		}).keyup(function(e) {
        //			if(e.ctrlKey) isCtrl = false;
        //		});
        //	};
        //	$.ctrl('C', function() {
        //		$('#console').slideToggle();
        //	});
        //}
        //},
        accordToggle: function() {
            // delta/triangle clicks for accordion panels
            $('a.accordToggle').on('click', function() {
                if ($(this).hasClass('open')) {
                    $(this).removeClass('open');
                    svpApp.util.altAccordListener();
                } else {

                    $(this).addClass('open');
                    svpApp.util.altAccordListener();
                }
            });
        },
        expandToggle: function() {
            $('a.expand-toggle').on('click', function(e) {
                var that = $(this);
                if ($(this).hasClass('open')) {
                    var thatLink = $(that).attr('href');
                    if ($(this).hasClass('rightarrow')) { // header link  -  banker name
                        $(thatLink).hide(function() { $(that).removeClass('open'); });
                    } else { //clear any input when line is hidden
                        $('' + thatLink).find('input[type=text]').each(function() {
                            $(this).val('');
                        });
                        $('' + thatLink).find('select').each(function() {
                            $(this).val('');
                            $(this).trigger('change');
                        });
                        $('' + thatLink).find('input[type=radio]').each(function() {
                            $(this).prop('checked', false);
                        });
                        $(thatLink).hide(1, function() { $(that).removeClass('open'); });
                    }
                } else {
                    var thisLink = $(this).attr('href');
                    if ($(this).hasClass('rightarrow')) {
                        $(thisLink).show(function() { $(that).addClass('open'); });
                    } else {
                        $(thisLink).show(1, function() { $(that).addClass('open'); });
                    }
                }
                e.preventDefault();
                return false;
            });
            // for additional info based on selected value - use #TARGET id in 'data-toggle' attr 
            // use value="0" for collapse - any other value to expand
            $('select.expand-toggle').change(function() {
                var thisLink = $(this).attr('data-toggle');
                if ($(this).val() != 0) {
                    $(thisLink).slideDown();
                } else {
                    $(thisLink).slideUp();
                }
            });

        },
        addStar: function() {
            $('div.tabsouter div.tabsTop .tab.on').append('<img src="' + imgPath + 'img/red_star.png" style="margin-bottom:-4px;margin-left:5px;" />');
            $('tr#offer1 td.col2').addClass('redtext');
            $('tr#offer1 td.col1 input').css('background', 'url(' + imgPath + 'img/checkmark_red.png) no-repeat center center');
        },
        buildSelects: function() {
            $('span.select').remove();
            /* jQuery(document).on('change', '.select', function () {
                 $(this).next().remove();
                 var title = $(this).attr('title');
                 title = $('option:selected', this).text();
                 $(this).css({ 'z-index': 10, 'opacity': 0, '-webkit-appearance': 'none' });
                 val = $('option:selected', this).text();
                 $(this).next().text(val);
                 if ($(this).next().next()) {
                     $(this).next().next().text(val);
                 }
                 if ($(this).hasClass('disabled') || ($(this).attr('disabled') == 'disabled')) {
                     $(this).next('span.select').addClass('disabled');
                 } else {
                     $(this).next('span.select').removeClass('disabled');
                 }
 
                 if ($(this).hasClass('disabled') || ($(this).attr('disabled') == 'disabled')) {
                     $(this).prop("disabled", true);
                     $(this).after('<span class="select disabled">' + title + '</span>');
                 } else if ($(this).hasClass('valerror')) {
                     $(this).after('<span class="select valerror">' + title + '</span>');
 
                     if ($(this).parent().find('img').attr('src') == null) {
                         $(this).next('span.select').after('<img src="' + imgPath + 'img/red_validation_error_arrow.png"  class="valarrow" style="width:12px; height:9px; padding: 0px; margin-top:8px; display:block;">');
                     }
                 } else if ($(this).hasClass('select-green')) {
                     $(this).after('<span class="select select-green">' + title + '</span>');
                 } else {
                     $(this).after('<span class="select">' + title + '</span>');
                 }
             }
             );
           */
            $('select.select').each(function() {
                var title = $(this).attr('title');
                title = $('option:selected', this).text();
                $(this)
                    .css({ 'z-index': 10, 'opacity': 0, '-webkit-appearance': 'none' })
                    .change(function() {
                        val = $('option:selected', this).text();
                        $(this).next().text(val);
                        if ($(this).next().next()) {
                            $(this).next().next().text(val);
                        }
                        if ($(this).hasClass('disabled') || ($(this).attr('disabled') == 'disabled')) {
                            $(this).next('span.select').addClass('disabled');
                        } else {
                            $(this).next('span.select').removeClass('disabled');
                        }
                    });
                if ($(this).hasClass('disabled') || ($(this).attr('disabled') == 'disabled')) {
                    $(this).prop("disabled", true);
                    $(this).after('<span class="select disabled">' + title + '</span>');
                } else if ($(this).hasClass('valerror')) {
                    $(this).after('<span class="select valerror">' + title + '</span>');

                    if ($(this).parent().find('img').attr('src') == null) {
                        $(this).next('span.select').after('<img src="' + imgPath + 'img/red_validation_error_arrow.png"  class="valarrow" style="width:12px; height:9px; padding: 0px; margin-top:8px; display:block;">');
                    }
                } else if ($(this).hasClass('select-green')) {
                    $(this).after('<span class="select select-green">' + title + '</span>');
                } else {
                    $(this).after('<span class="select">' + title + '</span>');
                }
            });

        },
        buildValErrors: function() {
            $('input.valerror').each(function() {
                if ($(this).parent().find('img').attr('src') == null) {
                    $(this).after('<img src="' + imgPath + 'img/red_validation_error_arrow.png"  class="valarrow" style="width:12px; height:9px; padding: 0px; display:inline-block;">');
                }
            });
            $('.valerror').not('dd, form, input, select').each(function() {
                if ($(this).parent().find('img').attr('src') == null) {
                    $(this).after('<img src="' + imgPath + 'img/red_validation_error_arrow.png"  class="valarrow" style="width:12px; height:9px; padding: 0px; margin-top:8px; display:block;">');
                }
            });
            $('dd.valerror, dd.valwarning').each(function() {
                if ($(this).children('.valarrow').length < 1) {
                    $(this).children('input, span.select').after('<img src="' + imgPath + 'img/red_validation_error_arrow.png"  class="valarrow">');
                }
            });
        },
        updateCountdown: function(countLimit, thatID, thisID) {
            var remaining = 0;
            var oldEl = $('#' + thisID);
            var oldLength = parseInt($('#' + thisID).val().length, 10);
            remaining = parseInt(countLimit - oldLength);
            if (countLimit === oldLength) {
                $(thatID).text('Maximum of ' + countLimit + ' characters reached');
                $(thatID).addClass('bolded');
                $(thatID).removeClass('redtext');
            } else if (oldLength < countLimit) {
                $(thatID).text(oldLength + ' characters used of ' + countLimit + ' allowed');
                $(thatID).removeClass('bolded');
                $(thatID).removeClass('redtext');
            } else {
                remaining = remaining * -1;
                $(thatID).text('Maximum of ' + countLimit + ' characters exceeded by ' + remaining);
                $(thatID).addClass('redtext');

            }
        },
        charCounter: function(target) {
            // <textarea> (&& <input>s) COUNTer mechanism (just add 'data-countlimit="200"' and '.countthis' class to use)
            if (target == undefined) {
                $('.countthis').each(function(index) {
                    if ($(this).next('span.charcount').length < 1) {
                        $(this).parent().append('<span class="charcount" id="count' + index + '"></span>');
                    }
                    //$(this).attr('id','texta'+index);
                    var thisID = $(this).attr('id');
                    var thatID = '#count' + index;
                    var countLimit = parseInt($(this).attr('data-countlimit'));
                    svpApp.util.updateCountdown(countLimit, thatID, thisID);
                    $('#' + thisID).off('keyup');
                    $('#' + thisID).on('keyup', function() {
                        svpApp.util.updateCountdown(countLimit, thatID, thisID);
                    });
                });
            } else {
                $('.countthis').each(function(index) {
                    var thisID = $(this).attr('id');
                    var countLimit = parseInt($(this).attr('data-countlimit'));
                    var thatID = target;
                    svpApp.util.updateCountdown(countLimit, thatID, thisID);
                    $('#' + thisID).off('keyup');
                    $('#' + thisID).on('keyup', function() {
                        svpApp.util.updateCountdown(countLimit, thatID, thisID);
                    });
                });
            }
        },
        getDate: function() {
            // ----- get todays date in Day, Month Date format

            var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

            var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

            Date.prototype.getMonthName = function() {
                return months[this.getMonth()];
            };
            Date.prototype.getDayName = function() {
                return days[this.getDay()];
            };
            var now = new Date();
            var day = now.getDayName();
            var month = now.getMonthName();
            var datenum = now.getDate();
            var myhours = now.getHours();
            var myminutes = now.getMinutes();
            var displayDate = day + ", " + month + " " + datenum;
            var future_date = new Date();
            future_date.setMonth(future_date.getMonth() + 3);
            var future_date_month = future_date.getMonth() + 1;
            var future_date_day = future_date.getDay();
            var nowmm = now.getMonth() + 1;
            var nowdd = now.getDate();
            if (future_date_month < 10) {
                future_date_month = "0" + future_date_month;
            }
            if (myminutes < 10) {
                myminutes = "0" + myminutes;
            }
            var ampm;
            if (myhours > 12) {
                myhours = myhours - 12;
                ampm = "pm";
            } else {
                ampm = "am";
            }
            var displayTime = myhours + ":" + myminutes + " " + ampm;
            if (nowmm < 10) {
                nowmm = "0" + nowmm;
            }
            if (nowdd < 10) {
                nowdd = "0" + nowdd;
            }
            if (future_date_month < 10) {
                future_date_month = "0" + future_date_month;
            }
            $('#headerDate').html(displayDate);
            $('.todaysDate').html(month + ' ' + datenum + ', ' + now.getFullYear());
            $('.todaymmddyyyy').html(nowmm + '/' + nowdd + '/' + now.getFullYear());
            $('.futureDate').html(future_date_month + '/' + future_date_day + '/' + future_date.getFullYear());
            $('.thetimeis').html(displayTime);
        },
        cookies: {
            get: function(key) {
                var keyValue = document.cookie.match('(^|;) ?' + key + '=([^;]*)(;|$)');
                return keyValue ? keyValue[2] : null;
            },
            set: function(key, value) {
                var expires = new Date();
                expires.setTime(expires.getTime() + 86400000); // 1 day
                document.cookie = key + '=' + value + ';expires=' + expires.toUTCString();
            }
        },
        phoneMask: function() {
            $('input.phonemask').mask('(999) 999-9999');
        },
        number9mask: function() {
            $('input.number9mask').mask('999999999', { placeholder: " " });
        },
        number10mask: function() {
            $('input.number10mask').mask('9999999999', { placeholder: " " });
        },
        emailMask: function() {
            $('input.emailmask').mask('?@?', { placeholder: " " });
        },
        ssnMask: function() {
            $('input.ssnmask').mask('999-99-9999', { placeholder: " " });
        },
        dateMask: function() {
            $("input.datemask").mask("99/99/9999", { placeholder: " " });
        },
        MMYYYYMask: function() {
            $("input.MMYYYYMask").mask("99/9999", { placeholder: " " });
        },
        MMDDYYYYMask: function() {
            $("input.MMDDYYYYMask").mask("99999999", { placeholder: " " });
        },
        zipMask: function() {
            $("input.zipmask").mask("99999", { placeholder: " " });
        },
        notEmpty: function() {
            // enables/disables the + circlebutton on the fullfillment page fields (phonefield)
            $('.notempty').each(function(i) {
                var thisTarget = $(this).attr('data-target');
                if ($(this).val() != '') {
                    $(thisTarget).removeClass('disabled');
                    $(this).siblings(thisTarget).removeClass('disabled');
                } else {
                    $(this).siblings(thisTarget).addClass('disabled');
                }
            });
        },
        captureSignature: function() {
            $('#review1 #sigcap').signature();
            $('#review2 #sigcap2').signature();
            $('.clearsig').on('click', function() {
                $('#sigcap').signature('clear');
            });
            $('.clearsig2').on('click', function() {
                $('#sigcap2').signature('clear');
            });
        },
        tinToggle: {
            init: function() {
                var tinTogID = '';
                $('.tinObfuscate').each(function(i) {
                    if ($(this).attr('data-tinCount') > 0) {
                        svpApp.util.tinToggle.tinCount = $(this).attr('data-tinCount');
                    } else {
                        svpApp.util.tinToggle.tinCount = 3;
                    }
                    if ($(this).next('a.toggleTin').length < 1) {
                        $(this).parent().append('<a href="javascript:;" class="toggleTin" rel=" " id="tinTog0' + i + '" data-tinCount="' + svpApp.util.tinToggle.tinCount + '">View</a>');
                    }
                });
                $('a.toggleTin').on('click', function() {
                    var tinMax = $(this).attr('data-tinCount');
                    if (tinMax > 0) {
                        tinTogID = $(this).attr('id');
                        var el = $('#' + tinTogID);
                        var newText = $(this).prev('.tinObfuscate').children('span.tinNumber').attr('data-fulltin');
                        var oldText = $(this).prev('.tinObfuscate').children('span.tinNumber').html();
                        var tinType = $(this).prev('.tinObfuscate').attr('data-type');
                        var toggleLabel = $(this).html();
                        var toggleAltLabel = $(this).attr('rel');
                        $(this).siblings('.tinObfuscate').find('span.tinNumber').html(newText);
                        $(this).siblings('.tinObfuscate').find('span.tinNumber').addClass('highlighted').attr('data-fulltin', oldText);
                        $(this).html(toggleAltLabel);
                        $(this).attr('rel', toggleLabel);
                        var newTinMax = (tinMax - 1);
                        $(this).attr('data-tinCount', newTinMax);
                        svpApp.util.tinToggle.tinTimer.start(el);
                    } else {
                        svpApp.modal.open("#", "", "<div class='alert alert-info-level1'><p>Information has been viewed the maximum number of times allowed.</p></div>", "Close", "");
                    }
                });
                return true;
            },
            tinTimer: {
                start: function(el) {
                    setTimeout(function() {
                        svpApp.util.tinToggle.tinTimer.end(el);
                    }, 5000);
                },
                end: function(el) {
                    var newText = $(el).prev('.tinObfuscate').children('span.tinNumber').attr('data-fulltin');
                    var oldText = $(el).prev('.tinObfuscate').children('span.tinNumber').html();
                    var tinType = $(el).prev('.tinObfuscate').attr('data-type');
                    var toggleLabel = $(el).html();
                    var toggleAltLabel = $(el).attr('rel');
                    $(el).siblings('.tinObfuscate').find('span.tinNumber').html(newText);
                    $(el).siblings('.tinObfuscate').find('span.tinNumber').removeClass('highlighted').attr('data-fulltin', oldText);
                    $(el).html(toggleAltLabel);
                    $(el).attr('rel', toggleLabel);
                }
            },
            tinCount: 3
        },
        toolTips: {
            init: function() {


                $('a.tip-anchor').tooltip({ html: true });
                $('a.infoicon').off(); // kills any current click/hover listener

                //spf - set these two values true here so we can debug popovers as if they were MetroStyle - for testing on Desktop ONLY
                //svpApp.util.isIE = true;
                //svpApp.util.isTouch = true;

                if (svpApp.util.isIE && svpApp.util.isTouch) {
                    // setup CLICKable popup

                    //spf- DO NOT! use 'animation:false' as a parameter for the popover call below, for some reason
                    //it blows up the ability to hide the next clicked  popup - weird!!!
                    $('a.infoicon').popover({ html: true, container: 'body' });

                    //spf - Bug EBSIM00128604 - hide and remove popovers from DOM on anywhere else but the icon
                    //      itself - THIS CODE CAN NOT BE CHANGED IN ANY WAY AS BOTH body functions handle
                    //      their own separate functionality to meet the specs.
                    //$('[data-toggle="popover"]').popover();

                    $('body').on('click', function(e) {
                        $('[data-toggle="popover"]').each(function() {
                            //the 'is' for buttons that trigger popups
                            //the 'has' for icons within a button that triggers a popup
                            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                                $(this).popover('hide');
                            }
                        });
                    });

                    //spf - 03/21/2014 - handle any hyperlink clicks for Surface and hide/remove from DOM any open popovers
                    $('a').on('click', function(e) {
                        $('[data-toggle="popover"]').each(function() {
                            //the 'is' for buttons that trigger popups
                            //the 'has' for icons within a button that triggers a popup
                            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                                $(this).popover('hide');
                            }
                        });
                        var popovers = $('.popover').not('.in');
                        if (popovers) {
                            popovers.remove();
                        }
                    });

                    $('body').on('hidden.bs.popover', function() {
                        var popovers = $('.popover').not('.in');
                        if (popovers) {
                            popovers.remove();
                        }
                    });


                    $('a.infoicon').on('shown.bs.popover', function(e) {

                        //spf - 2/12/2014 - need to hide any open popups before opening any new popups
                        //spf - discovered this as a bug on 2/7/2014, put into ClearQuest Tracking 2/10/2014
                        $('a.infoicon').not(this).popover('hide');

                        //spf - 2/13/2014 - This new section removes the dynamically created content by Bootstrap from the DOM
                        //      for any previously opened popover, not the current popover
                        $('.popover').each(function() {
                            //Only do this for all popovers other than the current one that cause this event
                            if (!($(this).is(e.target) || $(this).has(e.target).length > 0)
                                && $(this).siblings('.popover').length !== 0
                                && $(this).siblings('.popover').has(e.target).length === 0) {
                                //Remove the previously created popover element from the DOM
                                !$(this).remove();
                            }
                        });


                        //spf - remove all of this old stuff and only adds the x (use Hex - &#x58; or Numerical - &#88; HTML value) close button to the H3 tag that Bootstrap creates dynamically
                        //var icoIndex = $(this).index();
                        //if($(this).closest('#accountsAccord').find('a.popclose').length < 1){
                        //$(this).closest('#accountsAccord').find('h3.popover-title').append('<a class="popclose" href="javascript:;">X</a>');
                        $('h3.popover-title').append('<a class="popclose" href="javascript:;">&#x58;</a>');
                        //svpApp.util.toolTips.setupClose(icoIndex);
                        svpApp.util.toolTips.setupClose();
                        //}
                    });

                } else {
                    // for all nonIE/nonTouch, use 'hover'
                    //$('a.infoicon').popover({ animation: false, html: true, container: 'body', trigger: 'hover' });
                }

            },
            setupClose: function() {
                $('a.popclose').on('click', function(e) {
                    $('.popover').fadeOut(250, function() { $(this).remove(); });
                    e.preventDefault();
                    return false;
                });
            }
        },
        plusMinusToggle: function() {
            $('a.plusminus-toggle').on('click', function() {
                if ($(this).hasClass('open')) {
                    $(this).siblings('h3.panel-title').find('a').trigger('click');
                    $(this).removeClass('open');
                } else {
                    $(this).siblings('h3.panel-title').find('a').trigger('click');
                    $(this).addClass('open');
                }
            });
            $('h3.panel-title').on('click', function() {
                $(this).next('a.plusminus-toggle').toggleClass('open');
            });
        },
        altAccordListener: function() {
            // tie-into the existing bootstrap click function to change direction of the arrow
            $('.panel').on('show.bs.collapse', function() {
                $(this).find('a.accordToggle').removeClass('collapsed');
            });
            // this makes sure the bootstrap classes stay in tune with my .accordToggle classes
            $('h4.panel-title').on('click', function() {
                if ($(this).find('a').hasClass('collapsed')) {
                    $(this).closest('.panel-heading').find('a.accordToggle').removeClass('collapsed');
                } else {
                    $(this).closest('.panel-heading').find('.accordToggle').addClass('collapsed');
                }
            });
        },
        expandAll: function() {
            $('a.expand-all').on('click', function() {
                if ($(this).hasClass('open')) {
                    $('div.tab-pane.active a.accordToggle').not('.collapsed').trigger('click');
                    $(this).removeClass('open');
                    $(this).html('Expand All Sections');
                } else {
                    $('div.tab-pane.active a.accordToggle.collapsed').trigger('click');
                    $(this).addClass('open');
                    $(this).html('Collapse All Sections');
                }
            });
        },
        expandAllnotab: function() {
            $('a.expand-all').on('click', function() {
                if ($(this).hasClass('open')) {
                    $('a.accordToggle').not('.collapsed').trigger('click');
                    $(this).removeClass('open');
                    $(this).html('Expand All Sections');
                } else {
                    $('a.accordToggle.collapsed').trigger('click');
                    $(this).addClass('open');
                    $(this).html('Collapse All Sections');
                }
            });
        },
        emptyAccordCheck: function() {
            var noneText = '<span class="dimmed font12"> â€“ None</span>';
            $('div.panel-group div.panel-body').each(function() {
                if ($(this).children('table').length < 1) {
                    $(this).parent('div.panel-collapse').prev('div.panel-heading').find('h4.panel-title').append(noneText).next('span.pull-right').hide().prev('h4.panel-title').addClass('dimmed').find('a').attr('href', 'javascript:;').css('cursor', 'default');
                }
            });
        },
        tableSelectable: function() {
            var tableArrow = '<span class="chevron-right"></span>';
            $('table.table-selectable > tbody > tr').hover(function() {
                // hover-in
                $(this).addClass('hovered');
                $(this).children('td:last').append(tableArrow);
                // row click listener
                $(this).on('click', function() {
                    var thisLink = $(this).children('td.col1').children('a').attr('href');
                    //$(this).children('td.col1').children('a').one('click');
                    window.location.assign(thisLink);
                });
            }, function() {
                // hover-out
                $(this).removeClass('hovered');
                $(this).children('td:last').find('span.chevron-right').remove();
            });

        },
        searchTable: function() {
            var tableArrow = '<span class="chevron-right2"></span>';
            $('.searchRow').hover(function() {
                // hover-in
                $(this).addClass('hovered');
                $(this).children('div:last').append(tableArrow);
            }, function() {
                // hover-out
                $(this).removeClass('hovered');
                $(this).children('div:last').find('span.chevron-right2').remove();
            });

        },
        tableSelectable_atWorkCode: function() {
            var tableArrow = '<span class="chevron-right"></span>';
            $('table.table-selectable-atworkcode > tbody > tr').hover(function() {
                // hover-in
                $(this).addClass('hovered');
                $(this).children('td:last').append(tableArrow);
                // row click listener
                $(this).on('click', function(e) {
                    var thisCode = $(this).children('td.col1').data('atworkcode');
                    $('.showthecode').val(thisCode);
                    svpApp.modal.close(svpApp.modal.selector);
                    e.preventDefault();
                    return false;
                });
            }, function() {
                // hover-out
                $(this).removeClass('hovered');
                $(this).children('td:last').find('span.chevron-right').remove();
            });

        },
        textTruncateListener: function() {
            $('span.ellipsis_text').on('click', function() {
                if ($(this).hasClass('expanded')) {
                    $(this).removeClass('expanded');
                } else {
                    $(this).addClass('expanded');
                }
            });
        },
        clearErrorStates: function() {
            $('.valerror').removeClass('valerror');
            $('.valwarning').removeClass('valwarning');
            svpApp.util.removeErrorArrows();
            svpApp.util.hideErrorMessage();
        },
        removeErrorArrows: function() {
            $('img.valarrow').remove();
        },
        hideErrorMessage: function() {
            $('div.alert.alert-danger:eq(0)').slideUp();
        },
        enterAddress: function() {
            $('.addrcountry').on('change', function() {
                var thisPanel = ($('.tab-pane.active').attr('id'));
                var screenhastabs = false;
                if (typeof thisPanel !== "undefined") {
                    screenhastabs = true;
                }
                var addressCountry = $(this).val();
                switch (addressCountry) {
                case "US":
                    if (screenhastabs) {
                        $('div.tab-pane.active .cityFieldLabel').text('City:');
                        $('div.tab-pane.active .cityFieldText').text('US addresses will be standardized to meet postal guidelines.');
                        $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                        $('div.tab-pane.active .showhideState').removeClass('hidden');
                        if (typeof $('div.tab-pane.active .js-addrzip').val() !== "undefined") {
                            $('div.tab-pane.active .js-addrzip').val($('div.tab-pane.active .js-addrzip').val().substring('0', '5'));
                        }
                        $('div.tab-pane.active .js-addrzip').attr('maxlength', '5');
                    } else {
                        $('.cityFieldLabel').text('City:');
                        $('.cityFieldText').text('US addresses will be standardized to meet postal guidelines.');
                        $('.cityFieldInfo').removeClass('hidden');
                        $('.showhideState').removeClass('hidden');
                        if (typeof $('.js-addrzip').val() !== "undefined") {
                            $('.js-addrzip').val($('.js-addrzip').val().substring('0', '5'));
                        }
                        $('.js-addrzip').attr('maxlength', '5');
                    }
                    break;
                case "CA":
                    if (screenhastabs) {
                        $('div.tab-pane.active .cityFieldLabel').text('City, Province, Postal Code:');
                        $('div.tab-pane.active .cityFieldText').text('Enter City, 2 character Canadian Province, Postal Code');
                        $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                        $('div.tab-pane.active .showhideState').addClass('hidden');
                        $('div.tab-pane.active .js-addrzip').attr('maxlength', '9');
                        $('div.tab-pane.active .js-addrstate').val('');
                        $('div.tab-pane.active .js-addrstate').trigger('change');
                    } else {
                        $('.cityFieldLabel').text('City, Province, Postal Code:');
                        $('.cityFieldText').text('Enter City, 2 character Canadian Province, Postal Code');
                        $('.cityFieldInfo').removeClass('hidden');
                        $('.showhideState').addClass('hidden');
                        $('.js-addrzip').attr('maxlength', '9');
                        $('.js-addrstate').val('');
                        $('.js-addrstate').trigger('change');
                    }
                    break;
                case "MX":
                    if (screenhastabs) {
                        $('div.tab-pane.active .cityFieldLabel').text('Postal Code, City, State/Territory:');
                        $('div.tab-pane.active .cityFieldText').text('Enter Postal Code, City and 3 character Mexican State/Territory');
                        $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                        $('div.tab-pane.active .showhideState').addClass('hidden');
                        $('div.tab-pane.active .js-addrzip').attr('maxlength', '9');
                        $('div.tab-pane.active .js-addrstate').val('');
                        $('div.tab-pane.active .js-addrstate').trigger('change');
                    } else {
                        $('.cityFieldLabel').text('Postal Code, City, State/Territory:');
                        $('.cityFieldText').text('Enter Postal Code, City and 3 character Mexican State/Territory');
                        $('.cityFieldInfo').removeClass('hidden');
                        $('.showhideState').addClass('hidden');
                        $('.js-addrzip').attr('maxlength', '9');
                        $('.js-addrstate').val('');
                        $('.js-addrstate').trigger('change');
                    }
                    break;
                default:
                    if (screenhastabs) {
                        $('div.tab-pane.active .cityFieldLabel').text('City/Province:');
                        $('div.tab-pane.active .cityFieldInfo').addClass('hidden');
                        $('div.tab-pane.active .cityFieldText').text('');
                        $('div.tab-pane.active .showhideState').addClass('hidden');
                        $('div.tab-pane.active .js-addrzip').attr('maxlength', '9');
                        $('div.tab-pane.active .js-addrstate').val('');
                        $('div.tab-pane.active .js-addrstate').trigger('change');
                    } else {
                        $('.cityFieldLabel').text('City/Province:');
                        $('.cityFieldInfo').addClass('hidden');
                        $('.cityFieldText').text('');
                        $('.showhideState').addClass('hidden');
                        $('.js-addrzip').attr('maxlength', '9');
                        $('.js-addrstate').val('');
                        $('.js-addrstate').trigger('change');
                    }
                    break;
                }
            });
        },
        employmentInfo: function() {
            $('.js-teammember').on('change', function() {
                pickedval = $(this).val();
                switch (pickedval) {
                case "yes":
                    $('div.tab-pane.active .js-employertxt').val('Wells Fargo');
                    $('div.tab-pane.active .js-employerinfo').removeClass('hidden');
                    $('div.tab-pane.active .js-empsincedate').removeClass('hidden');
                    break;
                default:
                    $('div.tab-pane.active .js-employertxt').val('');
                    $('div.tab-pane.active .js-employerinfo').addClass('hidden');
                    $('div.tab-pane.active .js-empsincedate').addClass('hidden');
                    break;
                }
            });
            $('.js-employertxt').on('keyup', function() {
                if ($(this).val() != '') {
                    $('div.tab-pane.active .js-employerinfo').removeClass('hidden');
                } else {
                    $('div.tab-pane.active .js-employerinfo').addClass('hidden');
                }
            });
            $('.js-collegename').on('keyup', function() {
                if ($(this).val() != '') {
                    $('div.tab-pane.active .js-degreeinfo').removeClass('hidden');
                } else {
                    $('div.tab-pane.active .js-degreeinfo').addClass('hidden');
                }
            });
        },
        verifyAddress: function() {
            var thisPanel = ($('.tab-pane.active').attr('id'));
            var screenhastabs = false;
            if (typeof thisPanel !== "undefined") {
                screenhastabs = true;
            }

            $('.verifyaddress').on('change', function() {
                var needtoVerify = $(this).val();
                switch (needtoVerify) {
                case "new":
                    if (screenhastabs) {
                        if ($('div.tab-pane.active .addrcountry').val() == "US") {
                            $('div.tab-pane.active .js-verifynewaddr').removeClass('hidden');
                            $('.nextbtn').addClass('disabled');
                            $('div.tab-pane.active .getfeat').addClass('disabled');
                            $('div.tab-pane.active .verifybtn').removeClass('disabled');
                            $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                            $('div.tab-pane.active .cityFieldText').text(' US addresses will be standardized to meet postal guidelines.');
                        } else {
                            $('div.tab-pane.active .js-verifynewaddr').addClass('hidden');
                            $('.nextbtn').removeClass('disabled');
                            $('div.tab-pane.active .getfeat').removeClass('disabled');
                            $('div.tab-pane.active .verifybtn').addClass('disabled');
                        }
                    } else {
                        if ($('.addrcountry').val() == "US") {
                            $('.js-verifynewaddr').removeClass('hidden');
                            $('.nextbtn').addClass('disabled');
                            $('div.tab-pane.active .getfeat').addClass('disabled');
                            $('.verifybtn').removeClass('disabled');
                            $('.cityFieldInfo').removeClass('hidden');
                            $('.cityFieldText').text(' US addresses will be standardized to meet postal guidelines.');
                        } else {
                            $('.js-verifynewaddr').addClass('hidden');
                            $('.nextbtn').removeClass('disabled');
                            $('div.tab-pane.active .getfeat').removeClass('disabled');
                            $('.verifybtn').addClass('disabled');
                        }
                    }
                    break;
                default:
                    if (screenhastabs) {
                        $('div.tab-pane.active .cleartheaddress').val('');
                        $('div.tab-pane.active .cleartheaddress').trigger('change');
                        $('div.tab-pane.active .addrcountry').val('US');
                        if ($('div.tab-pane.active .addrcountry').val() === null) {
                            $('div.tab-pane.active .addrcountry').val('');
                        }
                        $('div.tab-pane.active .addrcountry').trigger('change');
                        $('div.tab-pane.active .js-verifynewaddr').addClass('hidden');
                        $('div.tab-pane.active [class^="streetaddr"]').css('display', 'none');
                        $('div.tab-pane.active .streettoggle').removeClass('open');
                        $('.nextbtn').removeClass('disabled');
                        $('div.tab-pane.active .getfeat').removeClass('disabled');
                    } else {
                        $('.cleartheaddress').val('');
                        $('.cleartheaddress').trigger('change');
                        $('.addrcountry').val('US');
                        if ($('.addrcountry').val() === null) {
                            $('.addrcountry').val('');
                        }
                        $('.addrcountry').trigger('change');
                        $('.js-verifynewaddr').addClass('hidden');
                        $('[class^="streetaddr"]').css('display', 'none');
                        $('.streettoggle').removeClass('open');
                        $('.nextbtn').removeClass('disabled');
                        $('.getfeat').removeClass('disabled');
                    }
                }
            });
            $('.verifybtn').on('click', function() {
                if (screenhastabs) {
                    $('div.tab-pane.active .verifybtn').addClass('disabled');
                    $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                    $('div.tab-pane.active .cityFieldText').text(' Address has been standardized.');
                    $('.nextbtn').removeClass('disabled');
                    $('div.tab-pane.active .getfeat').removeClass('disabled');
                } else {
                    $('.verifybtn').addClass('disabled');
                    $('.cityFieldInfo').removeClass('hidden');
                    $('.cityFieldText').text(' Address has been standardized.');
                    $('.nextbtn').removeClass('disabled');
                    $('.getfeat').removeClass('disabled');
                }
            });
            $('.addrupdate').on('change', function() {
                var needtoVerify = $('.verifyaddress').val();
                if (screenhastabs) {
                    if ($('div.tab-pane.active .verifyaddress').val() == "new") {
                        if ($('div.tab-pane.active .addrcountry').val() == "US") {
                            $('div.tab-pane.active .js-verifynewaddr').removeClass('hidden');
                            $('.nextbtn').addClass('disabled');
                            $('div.tab-pane.active .getfeat').addClass('disabled');
                            $('div.tab-pane.active .verifybtn').removeClass('disabled');
                            $('div.tab-pane.active .cityFieldInfo').removeClass('hidden');
                            $('div.tab-pane.active .cityFieldText').text(' US addresses will be standardized to meet postal guidelines.');
                        } else {
                            $('div.tab-pane.active .js-verifynewaddr').addClass('hidden');
                            $('.nextbtn').removeClass('disabled');
                            $('div.tab-pane.active .getfeat').removeClass('disabled');
                            $('div.tab-pane.active .verifybtn').addClass('disabled');
                        }
                    }
                } else {
                    if ($('.verifyaddress').val() == "new") {
                        if ($('.addrcountry').val() == "US") {
                            $('.js-verifynewaddr').removeClass('hidden');
                            $('.nextbtn').addClass('disabled');
                            $('.getfeat').addClass('disabled');
                            $('.verifybtn').removeClass('disabled');
                            $('.cityFieldInfo').removeClass('hidden');
                            $('.cityFieldText').text(' US addresses will be standardized to meet postal guidelines.');
                        } else {
                            $('.js-verifynewaddr').addClass('hidden');
                            $('.nextbtn').removeClass('disabled');
                            $('.getfeat').removeClass('disabled');
                            $('.verifybtn').addClass('disabled');
                        }
                    }
                }
            });
            $('.addressiscorrect').on('click', function() {
                if ($(this).prop('checked')) {
                    $('.nextbtn').removeClass('disabled');
                } else {
                    $('.nextbtn').addClass('disabled');
                }
            });
        },
        daysSinceDaysPlus: function() {
            todayis = new Date();
            date1900 = new Date(1900, 0, 01);
            date1900_ms = date1900.getTime();
            todayin_ms = todayis.getTime();
            var difference_1900ms = todayin_ms - date1900_ms;
            var difference_1900 = Math.round(difference_1900ms / (1000 * 60 * 60 * 24));
            since1900 = '-' + difference_1900 + 'd';
            ddis = todayis.getDate();
            mmis = todayis.getMonth();
            yyyyis = todayis.getFullYear();
            yyyyis = parseInt(yyyyis);
            mmis = parseInt(mmis);
            ddis = parseInt(ddis);
            yearplus50 = yyyyis + 50;
            dateplus50 = new Date(yearplus50, mmis, ddis);
            dateplus50_ms = dateplus50.getTime();
            var difference_plus50ms = dateplus50_ms - todayin_ms;
            var difference_plus50 = Math.round(difference_plus50ms / (1000 * 60 * 60 * 24));
            plus50 = '+' + difference_plus50 + 'd';
        },
        datePickerBind: function() {

        },
        datePicker: function() {
            svpApp.util.daysSinceDaysPlus();
            $('.add-on').click(function(e) {
                var thatInputID = $(e.target).prev().attr('id');
                if (!thatInputID) {
                    thatInputID = $(e.target).prev().prev().attr('id');
                };
                var classList = $(e.target).attr('class').split(/\s+/);
                $.each(classList, function(index, item) {
                    if (item !== 'add-on' && item !== 'icon-calendar') {
                        var thisClass = item;
                        changeDate(thisClass, thatInputID);
                    }
                });
            });

            function changeDate(groupName, inputID) {
                var useGroup = '.' + groupName;
                var useID = '#' + inputID;
                $(useGroup).on("changeDate", function(e) {
                    var dateSelected = $(useGroup).datepicker("getDate");
                    if (dateSelected != "Invalid Date") {
                        var day = dateSelected.getDate();
                        if (day < 10) {
                            day = "0" + day;
                        }
                        var month = dateSelected.getMonth() + 1;
                        if (month < 10) {
                            month = "0" + month;
                        }
                        var year = dateSelected.getFullYear();
                        var data = month + "/" + day + "/" + year;
                        if (useGroup == '.degree-group') {
                            data = month + '/' + year;
                        };
                        $(useID).val(data);
                        $(useID).removeClass('redtext');
                    };
                });
            };

            $('.dob-group').datepicker({
                startDate: "-45625d",
                endDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right",
            });
            $('.issue-group').datepicker({
                startDate: since1900,
                endDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $('.issue-group2').datepicker({
                startDate: since1900,
                endDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $('.expiration-group').datepicker({
                startDate: "0d",
                endDate: plus50,
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $('.expiration-group2').datepicker({
                startDate: "0d",
                endDate: plus50,
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $('.since-group1').datepicker({
                endDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $('.since-group2').datepicker({
                endDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm/dd/yyyy",
                orientation: "auto right"
            });
            $(".degree-group").datepicker({
                startDate: "0d",
                todayBtn: false,
                todayHighlight: true,
                autoclose: true,
                format: "mm-yyyy",
                viewMode: "months",
                minViewMode: "months",
                startView: "month",
                orientation: "auto right"
            });
            //Date Today (global), Date Input and Date Picker Inits - if user has already input a value, need to set the datepicker to that date
            //No need to do date validation on these values as the backend should have already validated them during the save procedure
            today = new Date();
            dd = today.getDate();
            mm = today.getMonth() + 1;
            yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            };
            if (mm < 10) {
                mm = '0' + mm;
            };
            yyyy = parseInt(yyyy);
            mm = parseInt(mm);
            dd = parseInt(dd);
            dateToday = new Date(mm + "/" + dd + "/" + yyyy);

            function thisIsDate(txtDate) {
                var currVal = txtDate;
                if (currVal === '') {
                    return false;
                };
                var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                var dtArray = currVal.match(rxDatePattern);
                if (dtArray === null) {
                    return false;
                };
                dtMonth = dtArray[1];
                dtDay = dtArray[3];
                dtYear = dtArray[5];
                if (dtMonth < 1 || dtMonth > 12)
                    return false;
                else if (dtDay < 1 || dtDay > 31)
                    return false;
                else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                    return false;
                else if (dtMonth == 2) {
                    var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                    if (dtDay > 29 || (dtDay == 29 && !isleap))
                        return false;
                };
                return true;
            };

            if ($('#dob').length) {
                dobVal = $("#dob").val();
                dobVal = parseInt(dobVal.substring(0, 2)) + '/' + parseInt(dobVal.substring(3, 5)) + '/' + parseInt(dobVal.substring(6, 10));
                if (dobVal != "  /  /    " && thisIsDate(dobVal)) {
                    $('.dob-group').datepicker('setDate', new Date(dobVal));
                } else {
                    $('.dob-group').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#issueDate').length) {
                issueVal = $("#issueDate").val();
                issueVal = parseInt(issueVal.substring(0, 2)) + '/' + parseInt(issueVal.substring(3, 5)) + '/' + parseInt(issueVal.substring(6, 10));
                if (issueVal != "  /  /    " && thisIsDate(issueVal)) {
                    $('.issue-group').datepicker('setDate', new Date(issueVal));
                } else {
                    $('.issue-group').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#expirationDate').length) {
                expirationVal = $("#expirationDate").val();
                expirationVal = parseInt(expirationVal.substring(0, 2)) + '/' + parseInt(expirationVal.substring(3, 5)) + '/' + parseInt(expirationVal.substring(6, 10));
                if (expirationVal != "  /  /    " && thisIsDate(expirationVal)) {
                    $('.expiration-group').datepicker('setDate', new Date(expirationVal));
                } else {
                    $('.expiration-group').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#issueDate2').length) {
                issueVal2 = $("#issueDate2").val();
                issueVal2 = parseInt(issueVal2.substring(0, 2)) + '/' + parseInt(issueVal2.substring(3, 5)) + '/' + parseInt(issueVal2.substring(6, 10));
                if (issueVal2 != "  /  /    " && thisIsDate(issueVal2)) {
                    $('.issue-group2').datepicker('setDate', new Date(issueVal2));
                } else {
                    $('.issue-group2').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#expirationDate2').length) {
                expirationVal2 = $("#expirationDate2").val();
                expirationVal2 = parseInt(expirationVal2.substring(0, 2)) + '/' + parseInt(expirationVal2.substring(3, 5)) + '/' + parseInt(expirationVal2.substring(6, 10));
                if (expirationVal2 != "  /  /    " && thisIsDate(expirationVal2)) {
                    $('.expiration-group2').datepicker('setDate', new Date(expirationVal2));
                } else {
                    $('.expiration-group2').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#sinceDate1').length) {
                sinceDate1Val = $("#sinceDate1").val();
                sinceDate1Val = parseInt(sinceDate1Val.substring(0, 2)) + '/' + parseInt(sinceDate1Val.substring(3, 5)) + '/' + parseInt(sinceDate1Val.substring(6, 10));
                if (sinceDate1Val != "  /  /    " && thisIsDate(sinceDate1Val)) {
                    $('.since-group1').datepicker('setDate', new Date(sinceDate1Val));
                } else {
                    $('.since-group1').datepicker('setDate', new Date(dateToday));
                };
            };
            if ($('#sinceDate2').length) {
                sinceDate2Val = $("#sinceDate2").val();
                sinceDate2Val = parseInt(sinceDate2Val.substring(0, 2)) + '/' + parseInt(sinceDate2Val.substring(3, 5)) + '/' + parseInt(sinceDate2Val.substring(6, 10));
                if (sinceDate2Val != "  /  /    " && thisIsDate(sinceDate2Val)) {
                    $('.since-group2').datepicker('setDate', new Date(sinceDate2Val));
                } else {
                    $('.since-group2').datepicker('setDate', new Date(dateToday));
                };
            }; //end Date Input and datepicker Init//

            //each input field has its own keyup and date Validation function, as user can input different dates,
            //including partial dates at the same time, so each input has to keep track of data entered dynamically
            //so each 'isDatexxx' function is a closure for the 'keyup' calling function and is unique to that input field
            //and thusly can not be a shared function 
            $('#dob').on('keyup', function() {
                var txtVal = $('#dob').val();
                var countVal = $('#dob').val().trim().length;
                if (isDate(txtVal)) {
                    var year125 = yyyy - 125;
                    var year100 = yyyy - 100;
                    var dateToday = new Date(mm + "/" + dd + "/" + yyyy);
                    var date125 = new Date(mm + "/" + dd + "/" + year125);
                    var date100 = new Date(mm + "/" + dd + "/" + year100);
                    var datePicked = new Date(dtMonthDOB + "/" + dtDayDOB + "/" + dtYearDOB);

                    $('.dob-group').datepicker('setDate', new Date(datePicked));
                    if (datePicked <= date100 && datePicked >= date125) {
                        $('#ageRange').removeClass('hidden');
                    } else {
                        $('#ageRange').addClass('hidden');
                    };
                    if (countVal == 10 && datePicked > dateToday || countVal == 10 && datePicked < date125) {
                        $('#dob').addClass('redtext');
                        $('.dob-group').datepicker({
                            startDate: "-45625d",
                            endDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    }
                } else {
                    $('.dob-group').datepicker('remove');
                    $('.dob-group').datepicker({
                        startDate: "-45625d",
                        endDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $('#dob').addClass('redtext');
                    } else {
                        $('#dob').removeClass('redtext');
                        $('#ageRange').addClass('hidden');
                    };
                };

                function isDate(txtDateDOB) {
                    var currValDOB = txtDateDOB;
                    if (currValDOB === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currValDOB.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthDOB = dtArray[1];
                    dtDayDOB = dtArray[3];
                    dtYearDOB = dtArray[5];
                    if (dtMonthDOB < 1 || dtMonthDOB > 12)
                        return false;
                    else if (dtDayDOB < 1 || dtDayDOB > 31)
                        return false;
                    else if ((dtMonthDOB == 4 || dtMonthDOB == 6 || dtMonthDOB == 9 || dtMonthDOB == 11) && dtDayDOB == 31)
                        return false;
                    else if (dtMonthDOB == 2) {
                        var isleap = (dtYearDOB % 4 == 0 && (dtYearDOB % 100 != 0 || dtYearDOB % 400 == 0));
                        if (dtDayDOB > 29 || (dtDayDOB == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#dob").keyup(function() //

            $('#issueDate').on('keyup', function() {
                var txtVal = $("#issueDate").val();
                var countVal = $("#issueDate").val().trim().length;
                if (isDateI(txtVal)) {
                    var dateToday = new Date(mm + "/" + dd + "/" + yyyy);
                    var datePicked = new Date(dtMonthI + "/" + dtDayI + "/" + dtYearI);
                    $('.issue-group').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked > dateToday || countVal == 10 && datePicked < date1900) {
                        $("#issueDate").addClass('redtext');
                        $('.issue-group').datepicker('remove');
                        $('.issue-group').datepicker({
                            startDate: since1900,
                            endDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.issue-group').datepicker('remove');
                    $('.issue-group').datepicker({
                        startDate: since1900,
                        endDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $("#issueDate").addClass('redtext');
                    } else {
                        $("#issueDate").removeClass('redtext');
                    };
                };

                function isDateI(txtDate) {
                    var currVal = txtDate;
                    if (currVal === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currVal.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthI = dtArray[1];
                    dtDayI = dtArray[3];
                    dtYearI = dtArray[5];
                    if (dtMonthI < 1 || dtMonthI > 12)
                        return false;
                    else if (dtDayI < 1 || dtDayI > 31)
                        return false;
                    else if ((dtMonthI == 4 || dtMonthI == 6 || dtMonthI == 9 || dtMonthI == 11) && dtDayI == 31)
                        return false;
                    else if (dtMonthI == 2) {
                        var isleap = (dtYearI % 4 == 0 && (dtYearI % 100 != 0 || dtYearI % 400 == 0));
                        if (dtDayI > 29 || (dtDayI == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#issueDate").keyup(function()//

            $('#expirationDate').on('keyup', function() {
                var txtVal = $('#expirationDate').val();
                var countVal = $('#expirationDate').val().trim().length;
                if (isDateE(txtVal)) {
                    var dateToday = new Date(mm + "/" + dd + "/" + yyyy);
                    var datePicked = new Date(dtMonthE + "/" + dtDayE + "/" + dtYearE);
                    $('.expiration-group').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked < dateToday || countVal == 10 && datePicked > dateplus50) {
                        $('#expirationDate').addClass('redtext');
                        $('.expiration-group').datepicker('remove');
                        $('.expiration-group').datepicker({
                            startDate: "0d",
                            endDate: plus50,
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.expiration-group').datepicker('remove');
                    $('.expiration-group').datepicker({
                        startDate: "0d",
                        endDate: plus50,
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $('#expirationDate').addClass('redtext');
                    } else {
                        $('#expirationDate').removeClass('redtext');
                    };
                };

                function isDateE(txtDate) {
                    var currVal = txtDate;
                    if (currVal === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currVal.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthE = dtArray[1];
                    dtDayE = dtArray[3];
                    dtYearE = dtArray[5];
                    if (dtMonthE < 1 || dtMonthE > 12)
                        return false;
                    else if (dtDayE < 1 || dtDayE > 31)
                        return false;
                    else if ((dtMonthE == 4 || dtMonthE == 6 || dtMonthE == 9 || dtMonthE == 11) && dtDayE == 31)
                        return false;
                    else if (dtMonthE == 2) {
                        var isleap = (dtYearE % 4 == 0 && (dtYearE % 100 != 0 || dtYearE % 400 == 0));
                        if (dtDayE > 29 || (dtDayE == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#expirationDate").keyup(function()//

            $('#issueDate2').on('keyup', function() {
                var txtVal = $("#issueDate2").val();
                var countVal = $("#issueDate2").val().trim().length;
                if (isDateI2(txtVal)) {
                    var dateToday = new Date(mm + "/" + dd + "/" + yyyy);
                    var datePicked = new Date(dtMonthI2 + "/" + dtDayI2 + "/" + dtYearI2);
                    $('.issue-group2').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked > dateToday || countVal == 10 && datePicked < date1900) {
                        $("#issueDate2").addClass('redtext');
                        $('.issue-group2').datepicker('remove');
                        $('.issue-group2').datepicker({
                            startDate: since1900,
                            endDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.issue-group2').datepicker('remove');
                    $('.issue-group2').datepicker({
                        startDate: since1900,
                        endDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $("#issueDate2").addClass('redtext');
                    } else {
                        $("#issueDate2").removeClass('redtext');
                    };
                };

                function isDateI2(txtDate) {
                    var currVal = txtDate;
                    if (currVal === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currVal.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthI2 = dtArray[1];
                    dtDayI2 = dtArray[3];
                    dtYearI2 = dtArray[5];
                    if (dtMonthI2 < 1 || dtMonthI2 > 12)
                        return false;
                    else if (dtDayI2 < 1 || dtDayI2 > 31)
                        return false;
                    else if ((dtMonthI2 == 4 || dtMonthI2 == 6 || dtMonthI2 == 9 || dtMonthI2 == 11) && dtDayI2 == 31)
                        return false;
                    else if (dtMonthI2 == 2) {
                        var isleap = (dtYearI2 % 4 == 0 && (dtYearI2 % 100 != 0 || dtYearI2 % 400 == 0));
                        if (dtDayI2 > 29 || (dtDayI2 == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#issueDate2").keyup(function()//

            $('#expirationDate2').on('keyup', function() {
                var txtVal = $('#expirationDate2').val();
                var countVal = $('#expirationDate2').val().trim().length;
                if (isDateE2(txtVal)) {
                    var dateToday = new Date(mm + "/" + dd + "/" + yyyy);
                    var datePicked = new Date(dtMonthE2 + "/" + dtDayE2 + "/" + dtYearE2);
                    $('.expiration-group2').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked < dateToday || countVal == 10 && datePicked > dateplus50) {
                        $('#expirationDate2').addClass('redtext');
                        $('.expiration-group2').datepicker({
                            startDate: "0d",
                            endDate: plus50,
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.expiration-group2').datepicker('remove');
                    $('.expiration-group2').datepicker({
                        startDate: "0d",
                        endDate: plus50,
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $('#expirationDate2').addClass('redtext');
                    } else {
                        $('#expirationDate2').removeClass('redtext');
                    };
                };

                function isDateE2(txtDate) {
                    var currValE2 = txtDate;
                    if (currValE2 === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currValE2.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthE2 = dtArray[1];
                    dtDayE2 = dtArray[3];
                    dtYearE2 = dtArray[5];
                    if (dtMonthE2 < 1 || dtMonthE2 > 12)
                        return false;
                    else if (dtDayE2 < 1 || dtDayE2 > 31)
                        return false;
                    else if ((dtMonthE2 == 4 || dtMonthE2 == 6 || dtMonthE2 == 9 || dtMonthE2 == 11) && dtDayE2 == 31)
                        return false;
                    else if (dtMonthE2 == 2) {
                        var isleap = (dtYearE2 % 4 == 0 && (dtYearE2 % 100 != 0 || dtYearE2 % 400 == 0));
                        if (dtDayE2 > 29 || (dtDayE2 == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#expirationDate2").keyup(function()//

            $('#sinceDate1').on('keyup', function() {
                var txtVal = $("#sinceDate1").val();
                var countVal = $("#sinceDate1").val().trim().length;
                if (isDateSince1(txtVal)) {
                    var datePicked = new Date(dtMonthSince1 + "/" + dtDaySince1 + "/" + dtYearSince1);
                    $('.since-group1').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked > dateToday) {
                        $("#sinceDate1").addClass('redtext');
                        $('.since-group1').datepicker('remove');
                        $('.since-group1').datepicker({
                            endDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.since-group1').datepicker('remove');
                    $('.since-group1').datepicker({
                        endDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $("#sinceDate1").addClass('redtext');
                    } else {
                        $("#sinceDate1").removeClass('redtext');
                    };
                };

                function isDateSince1(txtDate) {
                    var currVal = txtDate;
                    if (currVal === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currVal.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthSince1 = dtArray[1];
                    dtDaySince1 = dtArray[3];
                    dtYearSince1 = dtArray[5];
                    if (dtMonthSince1 < 1 || dtMonthSince1 > 12)
                        return false;
                    else if (dtDaySince1 < 1 || dtDaySince1 > 31)
                        return false;
                    else if ((dtMonthSince1 == 4 || dtMonthSince1 == 6 || dtMonthSince1 == 9 || dtMonthSince1 == 11) && dtDaySince1 == 31)
                        return false;
                    else if (dtMonthSince1 == 2) {
                        var isleap = (dtYearSince1 % 4 == 0 && (dtYearSince1 % 100 != 0 || dtYearSince1 % 400 == 0));
                        if (dtDaySince1 > 29 || (dtDaySince1 == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#sinceDate1").keyup(function()//

            $('#sinceDate2').on('keyup', function() {
                var txtVal = $("#sinceDate2").val();
                var countVal = $("#sinceDate2").val().trim().length;
                if (isDateSince2(txtVal)) {
                    var datePicked = new Date(dtMonthSince2 + "/" + dtDaySince2 + "/" + dtYearSince2);
                    $('.since-group2').datepicker('setDate', new Date(datePicked));
                    if (countVal == 10 && datePicked > dateToday) {
                        $("#sinceDate2").addClass('redtext');
                        $('.since-group2').datepicker('remove');
                        $('.since-group2').datepicker({
                            endDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm/dd/yyyy",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.since-group2').datepicker('remove');
                    $('.since-group2').datepicker({
                        endDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm/dd/yyyy",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /  /    " & countVal == 10) {
                        $("#sinceDate2").addClass('redtext');
                    } else {
                        $("#sinceDate2").removeClass('redtext');
                    };
                };

                function isDateSince2(txtDate) {
                    var currVal = txtDate;
                    if (currVal === '') {
                        return false;
                    };
                    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
                    var dtArray = currVal.match(rxDatePattern);
                    if (dtArray === null) {
                        return false;
                    };
                    dtMonthSince2 = dtArray[1];
                    dtDaySince2 = dtArray[3];
                    dtYearSince2 = dtArray[5];
                    if (dtMonthSince2 < 1 || dtMonthSince2 > 12)
                        return false;
                    else if (dtDaySince2 < 1 || dtDaySince2 > 31)
                        return false;
                    else if ((dtMonthSince2 == 4 || dtMonthSince2 == 6 || dtMonthSince2 == 9 || dtMonthSince2 == 11) && dtDaySince2 == 31)
                        return false;
                    else if (dtMonthSince2 == 2) {
                        var isleap = (dtYearSince2 % 4 == 0 && (dtYearSince2 % 100 != 0 || dtYearSince2 % 400 == 0));
                        if (dtDaySince2 > 29 || (dtDaySince2 == 29 && !isleap))
                            return false;
                    };
                    return true;
                };
            }); //end $("#sinceDate2").keyup(function()//

            $('#degreeDate').on('keyup', function() {
                var txtVal = $("#degreeDate").val();
                var countVal = $("#degreeDate").val().trim().length;
                $("#degreeDate").removeClass('redtext');
                if (validateDate(txtVal)) {
                    $("#degreeDate").removeClass('redtext');
                    var today = new Date();
                    var mm = today.getMonth() + 1;
                    var yyyy = today.getFullYear();
                    if (mm < 10) {
                        mm = '0' + mm;
                    };
                    var monthPicked = txtVal.substring(0, 2);
                    var yearPicked = txtVal.substring(3, 7);
                    if (yearPicked < yyyy || monthPicked < mm && yearPicked == yyyy) {
                        $("#degreeDate").addClass('redtext');
                        $('.degree-group').datepicker('remove');
                        $('.degree-group').datepicker({
                            startDate: "0d",
                            todayBtn: false,
                            todayHighlight: true,
                            autoclose: true,
                            format: "mm-yyyy",
                            viewMode: "months",
                            minViewMode: "months",
                            orientation: "auto right"
                        });
                    };
                } else {
                    $('.degree-group').datepicker('remove');
                    $('.degree-group').datepicker({
                        startDate: "0d",
                        todayBtn: false,
                        todayHighlight: true,
                        autoclose: true,
                        format: "mm-yyyy",
                        viewMode: "months",
                        minViewMode: "months",
                        orientation: "auto right"
                    });
                    if (txtVal != "  /    " & countVal == 7) {
                        $("#degreeDate").addClass('redtext');
                    } else {
                        $("#degreeDate").removeClass('redtext');
                    };
                };

                function validateDate(txtDate) {
                    var txtVal = txtDate;
                    var filter = new RegExp("(0[123456789]|10|11|12)([/])([0-3][0-9][0-9][0-9])");
                    if (filter.test(txtVal)) {
                        return true;
                    };
                    return false;

                };
            }); //end $("#degreeDate").keyup(function()//  
        }, //end svpApp.util.datePicker() function
        custRecID: function() {
            $('.tintype').on('change', function() {
                if ($('.tintype').val() > 2 || $('.tintype').val() == '') {
                    $('.tinnumber').addClass('hidden');
                    $('.js-tinnumval').val('');
                } else {
                    $('.tinnumber').removeClass('hidden');
                }
                if ($('.tintype').val() == 3) {
                    $('.nonuscitizen').removeClass('hidden');
                } else {
                    $('.nonuscitizen').addClass('hidden');
                }
            });

            $('.primaryID').on('change', function() {
                pickedIDval = $(this).val();
                thisID = "prime";
                $('.IDsecond').removeClass('hidden');
                resetIDvalues(thisID, pickedIDval);
            });
            $('.secondaryID').on('change', function() {
                pickedIDval = $(this).val();
                thisID = "second";
                $('.IDsecond').removeClass('hidden');
                resetIDvalues(thisID, pickedIDval);
            });
            $('#valprimeIDsubtype').on('change', function() {
                pickedSubType = $(this).val();
                pickedType = $('.primaryID').val();
                thisID = "prime";
                showIDsubtypefields(thisID, pickedSubType, pickedType);
            });
            $('#valsecondIDsubtype').on('change', function() {
                pickedSubType = $(this).val();
                thisID = "second";
                pickedType = $('.secondaryID').val();
                showIDsubtypefields(thisID, pickedSubType, pickedType);
            });

            function resetIDvalues(thisID, pickedval) {
                // clear all values on change of id type selected
                $("[class^='" + thisID + "ID']").addClass('hidden');
                $('#val' + thisID + 'IDsubtype').val('');
                $('.val' + thisID + 'IDdesc').val('');
                $('.val' + thisID + 'IDstate').val('');
                $('.val' + thisID + 'IDissue').val('');
                $('.val' + thisID + 'IDexp').val('');
                $('.val' + thisID + 'IDcountry').val('');
                $('.val' + thisID + 'IDprovince').val('');
                $('.val' + thisID + 'IDstate').trigger('change');
                $('.val' + thisID + 'IDprovince').trigger('change');
                switch (pickedval) {
                case "":
                    break;
                case "DLIC":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDstate').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "NDLC":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDstate').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "PASP":
                    svpApp.util.buildIDcountry(thisID, pickedval);
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDcountry').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                    break;
                case "ALID":
                case "FRID":
                case "AFID":
                case "MINR":
                case "NOTR":
                case "OTHR":
                    buildIDsubtype(thisID, pickedval);
                    break;

                default:
                    break;
                }
            }

            function buildIDsubtype(thisID, pickedval) {
                var options = '';
                var subopts = document.getElementById("val" + thisID + "IDsubtype");
                var opts = subopts.options.length;
                if (opts > 0) {
                    subopts.options.length = 0;
                }

                switch (pickedval) {
                case "FRID":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='CS'>Consular ID</option><option value='NI'>National identification</option><option value='DL'>Driver&#39;s License</option><option value='NL'>Non-driver&#39;s License</option>";
                        break;
                    case "second":
                        options = "<option selected value=''>Select</option><option value='CS'>Consular ID</option><option value='NI'>National identification</option><option value='DL'>Driver&#39;s License</option><option value='NL'>Non-driver&#39;s License</option><option value='VR'>Voter Registration Card</option><option value='OT'>Other</option>";
                        break;
                    }
                    break;
                case "ALID":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='BC'>B1/B2 Visa/Border Crossing Card</option><option value='EA'>Employment Authorization Card</option><option value='PR'>Permanent Resident Card</option><option value='RA'>Resident Alien Card</option>";
                        break;
                    case "second":
                        options = "<option selected value=''>Select</option><option value='BC'>B1/B2 Visa/Border Crossing Card</option><option value='EA'>Employment Authorization Card</option><option value='PR'>Permanent Resident Card</option><option value='RA'>Resident Alien Card</option><option value='OTHR'>Other</option>";
                        break;
                    }
                    break;
                case "AFID":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='AF'>US Air Force</option><option value='AR'>US Army</option><option value='CG'>US Coast Guard</option><option value='MR'>US Marines</option><option value='NV'>US Navy</option>";
                        break;
                    case "second":
                        options = "<option selected value=''>Select</option><option value='AF'>US Air Force</option><option value='AR'>US Army</option><option value='CG'>US Coast Guard</option><option value='MR'>US Marines</option><option value='NV'>US Navy</option><option value='FM'>Foreign Military</option><option value='OTHR'>Other</option>";
                        break;
                    }
                    break;
                case "MINR":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='BC'>Birth Certificate</option><option value='ST'>Student ID</option><option value='SS'>US Social Security Card</option><option value='OTHR'>Other</option>";
                        break;
                    case "second":
                        options = "<option value='IT'>Intro by adult relative or guardian</option>";
                        break;
                    }
                    break;
                case "NOTR":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='PA'>Power of Attorney</option><option value='AC'>Account Application</option>";
                        break;
                    case "second":
                        options = "<option selected value=''>Select</option><option value='PA'>Power of Attorney</option><option value='AC'>Account Application</option><option value='OTHR'>Other</option>";
                        break;
                    }
                    break;
                case "OTHR":
                    switch (thisID) {
                    case "prime":
                        options = "<option selected value=''>Select</option><option value='TR'>Tribal ID</option><option value='RR'>Review Required</option>";
                        break;
                    case "second":
                        options = "<option selected value=''>Select</option><option value='AT'>ATM Card</option><option value='CC'>Credit Card</option><option value='DC'>Debit Card</option><option value='EM'>Employer ID</option><option value='MB'>Membership Card</option><option value='MD'>Medical Card</option><option value='ST'>Student ID</option><option value='TR'>Tribal ID</option><option value='OTHR'>Other</option>";
                        break;
                    }
                    break;
                default:
                    break;
                }
                $('#val' + thisID + 'IDsubtype').html(options);
                $('#val' + thisID + 'IDsubtype').trigger('change');
                $('.' + thisID + 'IDsubtype').removeClass('hidden');
            }

            function showIDsubtypefields(thisID, pickedSubType, pickedType) {
                $("[class^='" + thisID + "ID']").addClass('hidden');
                $('.' + thisID + 'IDsubtype').removeClass('hidden');
                switch (pickedSubType) {
                case "":
                    break;

                case "CS":
                case "NI":
                case "OT":
                case "FM":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    svpApp.util.buildIDcountry(thisID, pickedSubType);
                    $('.' + thisID + 'IDcountry').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "DL":
                case "NL":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDprovince').removeClass('hidden');
                    svpApp.util.buildIDcountry(thisID, pickedSubType);
                    $('.' + thisID + 'IDcountry').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "VR":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    svpApp.util.buildIDcountry(thisID, pickedSubType);
                    $('.' + thisID + 'IDcountry').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "EM":
                case "MD":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDstate').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "BC":
                    switch (pickedType) {
                    case "MINR":
                        $('.' + thisID + 'IDdesc').removeClass('hidden');
                        $('.' + thisID + 'IDstate').removeClass('hidden');
                        $('.' + thisID + 'IDissue').removeClass('hidden');
                        $('.' + thisID + 'IDexp').removeClass('hidden');
                        break;
                    default:
                        $('.' + thisID + 'IDdesc').removeClass('hidden');
                        $('.' + thisID + 'IDissue').removeClass('hidden');
                        $('.' + thisID + 'IDexp').removeClass('hidden');
                        break;
                    }
                    break;
                case "OT":
                    switch (pickedType) {
                    case "NOTR":
                        $('.' + thisID + 'IDdesc').removeClass('hidden');
                        $('.' + thisID + 'IDissue').removeClass('hidden');
                        $('.' + thisID + 'IDexp').removeClass('hidden');
                        break;
                    default:
                        $('.' + thisID + 'IDdesc').removeClass('hidden');
                        svpApp.util.buildIDcountry(thisID, pickedSubType);
                        $('.' + thisID + 'IDcountry').removeClass('hidden');
                        $('.' + thisID + 'IDstate').removeClass('hidden');
                        $('.' + thisID + 'IDissue').removeClass('hidden');
                        $('.' + thisID + 'IDexp').removeClass('hidden');
                        break;
                    }
                    break;
                case "RR":
                    $('.val' + thisID + 'IDreviewid').val('');
                    $('.val' + thisID + 'IDreviewid').trigger('change');
                    svpApp.util.buildIDcountry(thisID, pickedSubType);
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDreviewinfo').removeClass('hidden');
                    $('.' + thisID + 'IDreviewid').removeClass('hidden');
                    $('.' + thisID + 'IDstate').removeClass('hidden');
                    $('.' + thisID + 'IDcountry').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                case "IT":
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDminorintro').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                default:
                    $('.' + thisID + 'IDdesc').removeClass('hidden');
                    $('.' + thisID + 'IDissue').removeClass('hidden');
                    $('.' + thisID + 'IDexp').removeClass('hidden');
                    break;
                }
            }
        },
        buildIDcountry: function(thisID, idtype) {
            var options = '';
            var countryopts = document.getElementById("val" + thisID + "IDcountry");
            var opts = countryopts.options.length;
            if (opts > 0) {
                countryopts.options.length = 0;
            }

            switch (idtype) {
            case "CS":
                options = "<option selected value=''>Select</option><option value='AR'>Argentina</option><option value='CO'>Colombia</option><option value='GT'>Guatemala</option><option value='MX'>Mexico</option>";
                break;
            case "DL":
            case "NL":
                options = "<option selected value='CA'>Canada</option>";
                break;
            case "NI":
                options = "<option selected value='SV'>El Salvador</option>";
                break;
            case "RR":
                options = "<option selected value=''>Select</option><option value='US'>United States</option>";
                break;
            case "USdefault":
                options = "<option selected value='US'>United States</option><option value='AF'>Afghanistan</option><option value='AX'>Aland Islands</option><option value='AL'>Albania</option><option value='DZ'>Algeria</option><option value='AD'>Andorra</option><option value='AO'>Angola</option><option value='AI'>Anguilla</option><option value='AQ'>Antarctica</option><option value='AG'>Antigua and Barbuda</option><option value='AR'>Argentina</option><option value='AM'>Armenia</option><option value='AW'>Aruba</option><option value='AU'>Australia</option><option value='AT'>Austria</option><option value='AZ'>Azerbaijan</option><option value='BS'>Bahamas</option><option value='BH'>Bahrain</option><option value='BD'>Bangladesh</option><option value='BB'>Barbados</option><option value='BY'>Belarus</option><option value='BE'>Belgium</option><option value='BZ'>Belize</option><option value='BJ'>Benin</option><option value='BM'>Bermuda</option><option value='BT'>Bhutan</option><option value='BO'>Bolivia</option><option value='BQ'>Bonaire, Saint Eustatius and Saba</option><option value='BA'>Bosnia and Herzegovina</option><option value='BW'>Botswana</option><option value='BV'>Bouvet Island</option><option value='BR'>Brazil</option><option value='IO'>British Indian Ocean Territory</option><option value='VG'>British Virgin Islands</option><option value='BN'>Brunei Darussalam</option><option value='BG'>Bulgaria</option><option value='BF'>Burkina Faso</option><option value='BI'>Burundi</option><option value='KH'>Cambodia</option><option value='CM'>Cameroon</option><option value='CA'>Canada</option><option value='CV'>Cape Verde</option><option value='KY'>Cayman Islands</option><option value='CF'>Central African Republic</option><option value='TD'>Chad</option><option value='CL'>Chile</option><option value='CN'>China</option><option value='CX'>Christmas Island</option><option value='CC'>Cocos (Keeling) Islands</option><option value='CO'>Colombia</option><option value='KM'>Comoros</option><option value='CD'>Congo, Democratic Republic of</option><option value='CG'>Congo, Republic of</option><option value='CK'>Cook Islands</option><option value='CR'>Costa Rica</option><option value='CI'>Cote d&#39;Ivoire (Ivory Coast)</option><option value='HR'>Croatia</option><option value='CU'>Cuba</option><option value='CW'>Curacao</option><option value='CY'>Cyprus</option><option value='CZ'>Czech Republic</option><option value='DK'>Denmark</option><option value='DJ'>Djibouti</option><option value='DM'>Dominica</option><option value='DO'>Dominican Republic</option><option value='EC'>Ecuador</option><option value='EG'>Egypt</option><option value='SV'>El Salvador</option><option value='GQ'>Equatorial Guinea</option><option value='ER'>Eritrea</option><option value='EE'>Estonia</option><option value='ET'>Ethiopia</option><option value='FK'>Falkland Islands (Malvinas)</option><option value='FO'>Faroe Islands</option><option value='FM'>Federated States of Micronesia</option><option value='FJ'>Fiji</option><option value='FI'>Finland</option><option value='FR'>France</option><option value='GF'>French Guiana</option><option value='PF'>French Polynesia</option><option value='TF'>French Southern Territories</option><option value='GA'>Gabon</option><option value='GM'>Gambia</option><option value='GE'>Georgia</option><option value='DE'>Germany</option><option value='GH'>Ghana</option><option value='GI'>Gibraltar</option><option value='GR'>Greece</option><option value='GL'>Greenland</option><option value='GD'>Grenada</option><option value='GP'>Guadeloupe</option><option value='GT'>Guatemala</option><option value='GG'>Guernsey</option><option value='GN'>Guinea</option><option value='GW'>Guinea-Bissau</option><option value='GY'>Guyana</option><option value='HT'>Haiti</option><option value='HM'>Heard and McDonald Islands</option><option value='VA'>Holy See (Vatican City State)</option><option value='HN'>Honduras</option><option value='HK'>Hong Kong</option><option value='HU'>Hungary</option><option value='IS'>Iceland</option><option value='IN'>India</option><option value='ID'>Indonesia</option><option value='IR'>Iran</option><option value='IQ'>Iraq</option><option value='IE'>Ireland</option><option value='IM'>Isle of Man</option><option value='IL'>Israel</option><option value='IT'>Italy</option><option value='JM'>Jamaica</option><option value='JP'>Japan</option><option value='JE'>Jersey</option><option value='JO'>Jordan</option><option value='KZ'>Kazakhstan</option><option value='KE'>Kenya</option><option value='KI'>Kiribati</option><option value='KO'>Kosovo</option><option value='KW'>Kuwait</option><option value='KG'>Kyrgyzstan</option><option value='LA'>Laos</option><option value='LV'>Latvia</option><option value='LB'>Lebanon</option><option value='LS'>Lesotho</option><option value='LR'>Liberia</option><option value='LY'>Libya</option><option value='LI'>Liechtenstein</option><option value='LT'>Lithuania</option><option value='LU'>Luxembourg</option><option value='MO'>Macao</option><option value='MK'>Macedonia</option><option value='MG'>Madagascar</option><option value='MW'>Malawi</option><option value='MY'>Malaysia</option><option value='MV'>Maldives</option><option value='ML'>Mali</option><option value='MT'>Malta</option><option value='MH'>Marshall Islands</option><option value='MQ'>Martinique</option><option value='MR'>Mauritania</option><option value='MU'>Mauritius</option><option value='YT'>Mayotte</option><option value='MX'>Mexico</option><option value='MD'>Moldova</option><option value='MC'>Monaco</option><option value='MN'>Mongolia</option><option value='ME'>Montenegro</option><option value='MS'>Montserrat</option><option value='MA'>Morocco</option><option value='MZ'>Mozambique</option><option value='MM'>Myanmar</option><option value='NA'>Namibia</option><option value='NR'>Nauru</option><option value='NP'>Nepal</option><option value='NL'>Netherlands</option><option value='NC'>New Caledonia</option><option value='NZ'>New Zealand</option><option value='NI'>Nicaragua</option><option value='NE'>Niger</option><option value='NG'>Nigeria</option><option value='NU'>Niue</option><option value='NF'>Norfolk Island</option><option value='KP'>North Korea</option><option value='NO'>Norway</option><option value='OM'>Oman</option><option value='PK'>Pakistan</option><option value='PW'>Palau</option><option value='PS'>Palestinian Territory, Occupied</option><option value='PA'>Panama</option><option value='PG'>Papua New Guinea</option><option value='PY'>Paraguay</option><option value='PE'>Peru</option><option value='PH'>Philippines</option><option value='PN'>Pitcairn</option><option value='PL'>Poland</option><option value='PT'>Portugal</option><option value='QA'>Qatar</option><option value='RE'>Reunion</option><option value='RO'>Romania</option><option value='RU'>Russian Federation</option><option value='RW'>Rwanda</option><option value='BL'>Saint Barthelemy</option><option value='SH'>Saint Helena</option><option value='KN'>Saint Kitts and Nevis</option><option value='LC'>Saint Lucia</option><option value='MF'>Saint Martin (French Part)</option><option value='PM'>Saint Pierre and Miquelon</option><option value='VC'>Saint Vincent and the Grenadines</option><option value='WS'>Samoa</option><option value='SM'>San Marino</option><option value='ST'>Sao Tome and Principe</option><option value='SA'>Saudi Arabia</option><option value='SN'>Senegal</option><option value='RS'>Serbia</option><option value='SC'>Seychelles</option><option value='SL'>Sierra Leone</option><option value='SG'>Singapore</option><option value='SX'>Sint Maarten</option><option value='SK'>Slovakia</option><option value='SI'>Slovenia</option><option value='SB'>Solomon Islands</option><option value='SO'>Somalia</option><option value='ZA'>South Africa</option><option value='GS'>South Georgia and South Sandwich Islands</option><option value='KR'>South Korea</option><option value='SS'>South Sudan</option><option value='ES'>Spain</option><option value='LK'>Sri Lanka</option><option value='SD'>Sudan</option><option value='SR'>Suriname</option><option value='SJ'>Svalbard and Jan Mayen</option><option value='SZ'>Swaziland</option><option value='SE'>Sweden</option><option value='CH'>Switzerland</option><option value='SY'>Syrian Arab Republic</option><option value='TW'>Taiwan</option><option value='TJ'>Tajikistan</option><option value='TZ'>Tanzania</option><option value='TH'>Thailand</option><option value='TL'>Timor - Leste</option><option value='TG'>Togo</option><option value='TK'>Tokelau</option><option value='TO'>Tonga</option><option value='TT'>Trinidad and Tobago</option><option value='TN'>Tunisia</option><option value='TR'>Turkey</option><option value='TM'>Turkmenistan</option><option value='TC'>Turks and Caicos Islands</option><option value='TV'>Tuvalu</option><option value='UG'>Uganda</option><option value='UA'>Ukraine</option><option value='AE'>United Arab Emirates</option><option value='GB'>United Kingdom</option><option value='UY'>Uruguay</option><option value='UM'>US Minor Outlying Islands</option><option value='UZ'>Uzbekistan</option><option value='VU'>Vanuata</option><option value='VE'>Venezuela</option><option value='VN'>Vietnam</option><option value='WF'>Wallis and Futuna</option><option value='EH'>Western Sahara</option><option value='YE'>Yemen</option><optionvalue='ZM'>Zambia</option><option value='ZW'>Zimbabwe</option>";
                break;
            case "noUS":
                options = "<option selected value=''>Select</option><option value='AF'>Afghanistan</option><option value='AX'>Aland Islands</option><option value='AL'>Albania</option><option value='DZ'>Algeria</option><option value='AD'>Andorra</option><option value='AO'>Angola</option><option value='AI'>Anguilla</option><option value='AQ'>Antarctica</option><option value='AG'>Antigua and Barbuda</option><option value='AR'>Argentina</option><option value='AM'>Armenia</option><option value='AW'>Aruba</option><option value='AU'>Australia</option><option value='AT'>Austria</option><option value='AZ'>Azerbaijan</option><option value='BS'>Bahamas</option><option value='BH'>Bahrain</option><option value='BD'>Bangladesh</option><option value='BB'>Barbados</option><option value='BY'>Belarus</option><option value='BE'>Belgium</option><option value='BZ'>Belize</option><option value='BJ'>Benin</option><option value='BM'>Bermuda</option><option value='BT'>Bhutan</option><option value='BO'>Bolivia</option><option value='BQ'>Bonaire, Saint Eustatius and Saba</option><option value='BA'>Bosnia and Herzegovina</option><option value='BW'>Botswana</option><option value='BV'>Bouvet Island</option><option value='BR'>Brazil</option><option value='IO'>British Indian Ocean Territory</option><option value='VG'>British Virgin Islands</option><option value='BN'>Brunei Darussalam</option><option value='BG'>Bulgaria</option><option value='BF'>Burkina Faso</option><option value='BI'>Burundi</option><option value='KH'>Cambodia</option><option value='CM'>Cameroon</option><option value='CA'>Canada</option><option value='CV'>Cape Verde</option><option value='KY'>Cayman Islands</option><option value='CF'>Central African Republic</option><option value='TD'>Chad</option><option value='CL'>Chile</option><option value='CN'>China</option><option value='CX'>Christmas Island</option><option value='CC'>Cocos (Keeling) Islands</option><option value='CO'>Colombia</option><option value='KM'>Comoros</option><option value='CD'>Congo, Democratic Republic of</option><option value='CG'>Congo, Republic of</option><option value='CK'>Cook Islands</option><option value='CR'>Costa Rica</option><option value='CI'>Cote d&#39;Ivoire (Ivory Coast)</option><option value='HR'>Croatia</option><option value='CU'>Cuba</option><option value='CW'>Curacao</option><option value='CY'>Cyprus</option><option value='CZ'>Czech Republic</option><option value='DK'>Denmark</option><option value='DJ'>Djibouti</option><option value='DM'>Dominica</option><option value='DO'>Dominican Republic</option><option value='EC'>Ecuador</option><option value='EG'>Egypt</option><option value='SV'>El Salvador</option><option value='GQ'>Equatorial Guinea</option><option value='ER'>Eritrea</option><option value='EE'>Estonia</option><option value='ET'>Ethiopia</option><option value='FK'>Falkland Islands (Malvinas)</option><option value='FO'>Faroe Islands</option><option value='FM'>Federated States of Micronesia</option><option value='FJ'>Fiji</option><option value='FI'>Finland</option><option value='FR'>France</option><option value='GF'>French Guiana</option><option value='PF'>French Polynesia</option><option value='TF'>French Southern Territories</option><option value='GA'>Gabon</option><option value='GM'>Gambia</option><option value='GE'>Georgia</option><option value='DE'>Germany</option><option value='GH'>Ghana</option><option value='GI'>Gibraltar</option><option value='GR'>Greece</option><option value='GL'>Greenland</option><option value='GD'>Grenada</option><option value='GP'>Guadeloupe</option><option value='GT'>Guatemala</option><option value='GG'>Guernsey</option><option value='GN'>Guinea</option><option value='GW'>Guinea-Bissau</option><option value='GY'>Guyana</option><option value='HT'>Haiti</option><option value='HM'>Heard and McDonald Islands</option><option value='VA'>Holy See (Vatican City State)</option><option value='HN'>Honduras</option><option value='HK'>Hong Kong</option><option value='HU'>Hungary</option><option value='IS'>Iceland</option><option value='IN'>India</option><option value='ID'>Indonesia</option><option value='IR'>Iran</option><option value='IQ'>Iraq</option><option value='IE'>Ireland</option><option value='IM'>Isle of Man</option><option value='IL'>Israel</option><option value='IT'>Italy</option><option value='JM'>Jamaica</option><option value='JP'>Japan</option><option value='JE'>Jersey</option><option value='JO'>Jordan</option><option value='KZ'>Kazakhstan</option><option value='KE'>Kenya</option><option value='KI'>Kiribati</option><option value='KO'>Kosovo</option><option value='KW'>Kuwait</option><option value='KG'>Kyrgyzstan</option><option value='LA'>Laos</option><option value='LV'>Latvia</option><option value='LB'>Lebanon</option><option value='LS'>Lesotho</option><option value='LR'>Liberia</option><option value='LY'>Libya</option><option value='LI'>Liechtenstein</option><option value='LT'>Lithuania</option><option value='LU'>Luxembourg</option><option value='MO'>Macao</option><option value='MK'>Macedonia</option><option value='MG'>Madagascar</option><option value='MW'>Malawi</option><option value='MY'>Malaysia</option><option value='MV'>Maldives</option><option value='ML'>Mali</option><option value='MT'>Malta</option><option value='MH'>Marshall Islands</option><option value='MQ'>Martinique</option><option value='MR'>Mauritania</option><option value='MU'>Mauritius</option><option value='YT'>Mayotte</option><option value='MX'>Mexico</option><option value='MD'>Moldova</option><option value='MC'>Monaco</option><option value='MN'>Mongolia</option><option value='ME'>Montenegro</option><option value='MS'>Montserrat</option><option value='MA'>Morocco</option><option value='MZ'>Mozambique</option><option value='MM'>Myanmar</option><option value='NA'>Namibia</option><option value='NR'>Nauru</option><option value='NP'>Nepal</option><option value='NL'>Netherlands</option><option value='NC'>New Caledonia</option><option value='NZ'>New Zealand</option><option value='NI'>Nicaragua</option><option value='NE'>Niger</option><option value='NG'>Nigeria</option><option value='NU'>Niue</option><option value='NF'>Norfolk Island</option><option value='KP'>North Korea</option><option value='NO'>Norway</option><option value='OM'>Oman</option><option value='PK'>Pakistan</option><option value='PW'>Palau</option><option value='PS'>Palestinian Territory, Occupied</option><option value='PA'>Panama</option><option value='PG'>Papua New Guinea</option><option value='PY'>Paraguay</option><option value='PE'>Peru</option><option value='PH'>Philippines</option><option value='PN'>Pitcairn</option><option value='PL'>Poland</option><option value='PT'>Portugal</option><option value='QA'>Qatar</option><option value='RE'>Reunion</option><option value='RO'>Romania</option><option value='RU'>Russian Federation</option><option value='RW'>Rwanda</option><option value='BL'>Saint Barthelemy</option><option value='SH'>Saint Helena</option><option value='KN'>Saint Kitts and Nevis</option><option value='LC'>Saint Lucia</option><option value='MF'>Saint Martin (French Part)</option><option value='PM'>Saint Pierre and Miquelon</option><option value='VC'>Saint Vincent and the Grenadines</option><option value='WS'>Samoa</option><option value='SM'>San Marino</option><option value='ST'>Sao Tome and Principe</option><option value='SA'>Saudi Arabia</option><option value='SN'>Senegal</option><option value='RS'>Serbia</option><option value='SC'>Seychelles</option><option value='SL'>Sierra Leone</option><option value='SG'>Singapore</option><option value='SX'>Sint Maarten</option><option value='SK'>Slovakia</option><option value='SI'>Slovenia</option><option value='SB'>Solomon Islands</option><option value='SO'>Somalia</option><option value='ZA'>South Africa</option><option value='GS'>South Georgia and South Sandwich Islands</option><option value='KR'>South Korea</option><option value='SS'>South Sudan</option><option value='ES'>Spain</option><option value='LK'>Sri Lanka</option><option value='SD'>Sudan</option><option value='SR'>Suriname</option><option value='SJ'>Svalbard and Jan Mayen</option><option value='SZ'>Swaziland</option><option value='SE'>Sweden</option><option value='CH'>Switzerland</option><option value='SY'>Syrian Arab Republic</option><option value='TW'>Taiwan</option><option value='TJ'>Tajikistan</option><option value='TZ'>Tanzania</option><option value='TH'>Thailand</option><option value='TL'>Timor - Leste</option><option value='TG'>Togo</option><option value='TK'>Tokelau</option><option value='TO'>Tonga</option><option value='TT'>Trinidad and Tobago</option><option value='TN'>Tunisia</option><option value='TR'>Turkey</option><option value='TM'>Turkmenistan</option><option value='TC'>Turks and Caicos Islands</option><option value='TV'>Tuvalu</option><option value='UG'>Uganda</option><option value='UA'>Ukraine</option><option value='AE'>United Arab Emirates</option><option value='GB'>United Kingdom</option><option value='UY'>Uruguay</option><option value='UM'>US Minor Outlying Islands</option><option value='UZ'>Uzbekistan</option><option value='VU'>Vanuata</option><option value='VE'>Venezuela</option><option value='VN'>Vietnam</option><option value='WF'>Wallis and Futuna</option><option value='EH'>Western Sahara</option><option value='YE'>Yemen</option><optionvalue='ZM'>Zambia</option><option value='ZW'>Zimbabwe</option>";
                break;
            case "FM":
            case "VR":
            case "OT":
                options = "<option selected value=''>Select</option><option value='AF'>Afghanistan</option><option value='AX'>Aland Islands</option><option value='AL'>Albania</option><option value='DZ'>Algeria</option><option value='AD'>Andorra</option><option value='AO'>Angola</option><option value='AI'>Anguilla</option><option value='AQ'>Antarctica</option><option value='AG'>Antigua and Barbuda</option><option value='AR'>Argentina</option><option value='AM'>Armenia</option><option value='AW'>Aruba</option><option value='AU'>Australia</option><option value='AT'>Austria</option><option value='AZ'>Azerbaijan</option><option value='BS'>Bahamas</option><option value='BH'>Bahrain</option><option value='BD'>Bangladesh</option><option value='BB'>Barbados</option><option value='BY'>Belarus</option><option value='BE'>Belgium</option><option value='BZ'>Belize</option><option value='BJ'>Benin</option><option value='BM'>Bermuda</option><option value='BT'>Bhutan</option><option value='BO'>Bolivia</option><option value='BQ'>Bonaire, Saint Eustatius and Saba</option><option value='BA'>Bosnia and Herzegovina</option><option value='BW'>Botswana</option><option value='BV'>Bouvet Island</option><option value='BR'>Brazil</option><option value='IO'>British Indian Ocean Territory</option><option value='VG'>British Virgin Islands</option><option value='BN'>Brunei Darussalam</option><option value='BG'>Bulgaria</option><option value='BF'>Burkina Faso</option><option value='BI'>Burundi</option><option value='KH'>Cambodia</option><option value='CM'>Cameroon</option><option value='CA'>Canada</option><option value='CV'>Cape Verde</option><option value='KY'>Cayman Islands</option><option value='CF'>Central African Republic</option><option value='TD'>Chad</option><option value='CL'>Chile</option><option value='CN'>China</option><option value='CX'>Christmas Island</option><option value='CC'>Cocos (Keeling) Islands</option><option value='CO'>Colombia</option><option value='KM'>Comoros</option><option value='CD'>Congo, Democratic Republic of</option><option value='CG'>Congo, Republic of</option><option value='CK'>Cook Islands</option><option value='CR'>Costa Rica</option><option value='CI'>Cote d&#39;Ivoire (Ivory Coast)</option><option value='HR'>Croatia</option><option value='CU'>Cuba</option><option value='CW'>Curacao</option><option value='CY'>Cyprus</option><option value='CZ'>Czech Republic</option><option value='DK'>Denmark</option><option value='DJ'>Djibouti</option><option value='DM'>Dominica</option><option value='DO'>Dominican Republic</option><option value='EC'>Ecuador</option><option value='EG'>Egypt</option><option value='SV'>El Salvador</option><option value='GQ'>Equatorial Guinea</option><option value='ER'>Eritrea</option><option value='EE'>Estonia</option><option value='ET'>Ethiopia</option><option value='FK'>Falkland Islands (Malvinas)</option><option value='FO'>Faroe Islands</option><option value='FM'>Federated States of Micronesia</option><option value='FJ'>Fiji</option><option value='FI'>Finland</option><option value='FR'>France</option><option value='GF'>French Guiana</option><option value='PF'>French Polynesia</option><option value='TF'>French Southern Territories</option><option value='GA'>Gabon</option><option value='GM'>Gambia</option><option value='GE'>Georgia</option><option value='DE'>Germany</option><option value='GH'>Ghana</option><option value='GI'>Gibraltar</option><option value='GR'>Greece</option><option value='GL'>Greenland</option><option value='GD'>Grenada</option><option value='GP'>Guadeloupe</option><option value='GT'>Guatemala</option><option value='GG'>Guernsey</option><option value='GN'>Guinea</option><option value='GW'>Guinea-Bissau</option><option value='GY'>Guyana</option><option value='HT'>Haiti</option><option value='HM'>Heard and McDonald Islands</option><option value='VA'>Holy See (Vatican City State)</option><option value='HN'>Honduras</option><option value='HK'>Hong Kong</option><option value='HU'>Hungary</option><option value='IS'>Iceland</option><option value='IN'>India</option><option value='ID'>Indonesia</option><option value='IR'>Iran</option><option value='IQ'>Iraq</option><option value='IE'>Ireland</option><option value='IM'>Isle of Man</option><option value='IL'>Israel</option><option value='IT'>Italy</option><option value='JM'>Jamaica</option><option value='JP'>Japan</option><option value='JE'>Jersey</option><option value='JO'>Jordan</option><option value='KZ'>Kazakhstan</option><option value='KE'>Kenya</option><option value='KI'>Kiribati</option><option value='KO'>Kosovo</option><option value='KW'>Kuwait</option><option value='KG'>Kyrgyzstan</option><option value='LA'>Laos</option><option value='LV'>Latvia</option><option value='LB'>Lebanon</option><option value='LS'>Lesotho</option><option value='LR'>Liberia</option><option value='LY'>Libya</option><option value='LI'>Liechtenstein</option><option value='LT'>Lithuania</option><option value='LU'>Luxembourg</option><option value='MO'>Macao</option><option value='MK'>Macedonia</option><option value='MG'>Madagascar</option><option value='MW'>Malawi</option><option value='MY'>Malaysia</option><option value='MV'>Maldives</option><option value='ML'>Mali</option><option value='MT'>Malta</option><option value='MH'>Marshall Islands</option><option value='MQ'>Martinique</option><option value='MR'>Mauritania</option><option value='MU'>Mauritius</option><option value='YT'>Mayotte</option><option value='MX'>Mexico</option><option value='MD'>Moldova</option><option value='MC'>Monaco</option><option value='MN'>Mongolia</option><option value='ME'>Montenegro</option><option value='MS'>Montserrat</option><option value='MA'>Morocco</option><option value='MZ'>Mozambique</option><option value='MM'>Myanmar</option><option value='NA'>Namibia</option><option value='NR'>Nauru</option><option value='NP'>Nepal</option><option value='NL'>Netherlands</option><option value='NC'>New Caledonia</option><option value='NZ'>New Zealand</option><option value='NI'>Nicaragua</option><option value='NE'>Niger</option><option value='NG'>Nigeria</option><option value='NU'>Niue</option><option value='NF'>Norfolk Island</option><option value='KP'>North Korea</option><option value='NO'>Norway</option><option value='OM'>Oman</option><option value='PK'>Pakistan</option><option value='PW'>Palau</option><option value='PS'>Palestinian Territory, Occupied</option><option value='PA'>Panama</option><option value='PG'>Papua New Guinea</option><option value='PY'>Paraguay</option><option value='PE'>Peru</option><option value='PH'>Philippines</option><option value='PN'>Pitcairn</option><option value='PL'>Poland</option><option value='PT'>Portugal</option><option value='QA'>Qatar</option><option value='RE'>Reunion</option><option value='RO'>Romania</option><option value='RU'>Russian Federation</option><option value='RW'>Rwanda</option><option value='BL'>Saint Barthelemy</option><option value='SH'>Saint Helena</option><option value='KN'>Saint Kitts and Nevis</option><option value='LC'>Saint Lucia</option><option value='MF'>Saint Martin (French Part)</option><option value='PM'>Saint Pierre and Miquelon</option><option value='VC'>Saint Vincent and the Grenadines</option><option value='WS'>Samoa</option><option value='SM'>San Marino</option><option value='ST'>Sao Tome and Principe</option><option value='SA'>Saudi Arabia</option><option value='SN'>Senegal</option><option value='RS'>Serbia</option><option value='SC'>Seychelles</option><option value='SL'>Sierra Leone</option><option value='SG'>Singapore</option><option value='SX'>Sint Maarten</option><option value='SK'>Slovakia</option><option value='SI'>Slovenia</option><option value='SB'>Solomon Islands</option><option value='SO'>Somalia</option><option value='ZA'>South Africa</option><option value='GS'>South Georgia and South Sandwich Islands</option><option value='KR'>South Korea</option><option value='SS'>South Sudan</option><option value='ES'>Spain</option><option value='LK'>Sri Lanka</option><option value='SD'>Sudan</option><option value='SR'>Suriname</option><option value='SJ'>Svalbard and Jan Mayen</option><option value='SZ'>Swaziland</option><option value='SE'>Sweden</option><option value='CH'>Switzerland</option><option value='SY'>Syrian Arab Republic</option><option value='TW'>Taiwan</option><option value='TJ'>Tajikistan</option><option value='TZ'>Tanzania</option><option value='TH'>Thailand</option><option value='TL'>Timor - Leste</option><option value='TG'>Togo</option><option value='TK'>Tokelau</option><option value='TO'>Tonga</option><option value='TT'>Trinidad and Tobago</option><option value='TN'>Tunisia</option><option value='TR'>Turkey</option><option value='TM'>Turkmenistan</option><option value='TC'>Turks and Caicos Islands</option><option value='TV'>Tuvalu</option><option value='UG'>Uganda</option><option value='UA'>Ukraine</option><option value='AE'>United Arab Emirates</option><option value='GB'>United Kingdom</option><option value='UY'>Uruguay</option><option value='UM'>US Minor Outlying Islands</option><option value='UZ'>Uzbekistan</option><option value='VU'>Vanuata</option><option value='VE'>Venezuela</option><option value='VN'>Vietnam</option><option value='WF'>Wallis and Futuna</option><option value='EH'>Western Sahara</option><option value='YE'>Yemen</option><optionvalue='ZM'>Zambia</option><option value='ZW'>Zimbabwe</option>";
                break;
            default:
                options = "<option selected value=''>Select</option><option value='US'>United States</option><option value='AF'>Afghanistan</option><option value='AX'>Aland Islands</option><option value='AL'>Albania</option><option value='DZ'>Algeria</option><option value='AD'>Andorra</option><option value='AO'>Angola</option><option value='AI'>Anguilla</option><option value='AQ'>Antarctica</option><option value='AG'>Antigua and Barbuda</option><option value='AR'>Argentina</option><option value='AM'>Armenia</option><option value='AW'>Aruba</option><option value='AU'>Australia</option><option value='AT'>Austria</option><option value='AZ'>Azerbaijan</option><option value='BS'>Bahamas</option><option value='BH'>Bahrain</option><option value='BD'>Bangladesh</option><option value='BB'>Barbados</option><option value='BY'>Belarus</option><option value='BE'>Belgium</option><option value='BZ'>Belize</option><option value='BJ'>Benin</option><option value='BM'>Bermuda</option><option value='BT'>Bhutan</option><option value='BO'>Bolivia</option><option value='BQ'>Bonaire, Saint Eustatius and Saba</option><option value='BA'>Bosnia and Herzegovina</option><option value='BW'>Botswana</option><option value='BV'>Bouvet Island</option><option value='BR'>Brazil</option><option value='IO'>British Indian Ocean Territory</option><option value='VG'>British Virgin Islands</option><option value='BN'>Brunei Darussalam</option><option value='BG'>Bulgaria</option><option value='BF'>Burkina Faso</option><option value='BI'>Burundi</option><option value='KH'>Cambodia</option><option value='CM'>Cameroon</option><option value='CA'>Canada</option><option value='CV'>Cape Verde</option><option value='KY'>Cayman Islands</option><option value='CF'>Central African Republic</option><option value='TD'>Chad</option><option value='CL'>Chile</option><option value='CN'>China</option><option value='CX'>Christmas Island</option><option value='CC'>Cocos (Keeling) Islands</option><option value='CO'>Colombia</option><option value='KM'>Comoros</option><option value='CD'>Congo, Democratic Republic of</option><option value='CG'>Congo, Republic of</option><option value='CK'>Cook Islands</option><option value='CR'>Costa Rica</option><option value='CI'>Cote d&#39;Ivoire (Ivory Coast)</option><option value='HR'>Croatia</option><option value='CU'>Cuba</option><option value='CW'>Curacao</option><option value='CY'>Cyprus</option><option value='CZ'>Czech Republic</option><option value='DK'>Denmark</option><option value='DJ'>Djibouti</option><option value='DM'>Dominica</option><option value='DO'>Dominican Republic</option><option value='EC'>Ecuador</option><option value='EG'>Egypt</option><option value='SV'>El Salvador</option><option value='GQ'>Equatorial Guinea</option><option value='ER'>Eritrea</option><option value='EE'>Estonia</option><option value='ET'>Ethiopia</option><option value='FK'>Falkland Islands (Malvinas)</option><option value='FO'>Faroe Islands</option><option value='FM'>Federated States of Micronesia</option><option value='FJ'>Fiji</option><option value='FI'>Finland</option><option value='FR'>France</option><option value='GF'>French Guiana</option><option value='PF'>French Polynesia</option><option value='TF'>French Southern Territories</option><option value='GA'>Gabon</option><option value='GM'>Gambia</option><option value='GE'>Georgia</option><option value='DE'>Germany</option><option value='GH'>Ghana</option><option value='GI'>Gibraltar</option><option value='GR'>Greece</option><option value='GL'>Greenland</option><option value='GD'>Grenada</option><option value='GP'>Guadeloupe</option><option value='GT'>Guatemala</option><option value='GG'>Guernsey</option><option value='GN'>Guinea</option><option value='GW'>Guinea-Bissau</option><option value='GY'>Guyana</option><option value='HT'>Haiti</option><option value='HM'>Heard and McDonald Islands</option><option value='VA'>Holy See (Vatican City State)</option><option value='HN'>Honduras</option><option value='HK'>Hong Kong</option><option value='HU'>Hungary</option><option value='IS'>Iceland</option><option value='IN'>India</option><option value='ID'>Indonesia</option><option value='IR'>Iran</option><option value='IQ'>Iraq</option><option value='IE'>Ireland</option><option value='IM'>Isle of Man</option><option value='IL'>Israel</option><option value='IT'>Italy</option><option value='JM'>Jamaica</option><option value='JP'>Japan</option><option value='JE'>Jersey</option><option value='JO'>Jordan</option><option value='KZ'>Kazakhstan</option><option value='KE'>Kenya</option><option value='KI'>Kiribati</option><option value='KO'>Kosovo</option><option value='KW'>Kuwait</option><option value='KG'>Kyrgyzstan</option><option value='LA'>Laos</option><option value='LV'>Latvia</option><option value='LB'>Lebanon</option><option value='LS'>Lesotho</option><option value='LR'>Liberia</option><option value='LY'>Libya</option><option value='LI'>Liechtenstein</option><option value='LT'>Lithuania</option><option value='LU'>Luxembourg</option><option value='MO'>Macao</option><option value='MK'>Macedonia</option><option value='MG'>Madagascar</option><option value='MW'>Malawi</option><option value='MY'>Malaysia</option><option value='MV'>Maldives</option><option value='ML'>Mali</option><option value='MT'>Malta</option><option value='MH'>Marshall Islands</option><option value='MQ'>Martinique</option><option value='MR'>Mauritania</option><option value='MU'>Mauritius</option><option value='YT'>Mayotte</option><option value='MX'>Mexico</option><option value='MD'>Moldova</option><option value='MC'>Monaco</option><option value='MN'>Mongolia</option><option value='ME'>Montenegro</option><option value='MS'>Montserrat</option><option value='MA'>Morocco</option><option value='MZ'>Mozambique</option><option value='MM'>Myanmar</option><option value='NA'>Namibia</option><option value='NR'>Nauru</option><option value='NP'>Nepal</option><option value='NL'>Netherlands</option><option value='NC'>New Caledonia</option><option value='NZ'>New Zealand</option><option value='NI'>Nicaragua</option><option value='NE'>Niger</option><option value='NG'>Nigeria</option><option value='NU'>Niue</option><option value='NF'>Norfolk Island</option><option value='KP'>North Korea</option><option value='NO'>Norway</option><option value='OM'>Oman</option><option value='PK'>Pakistan</option><option value='PW'>Palau</option><option value='PS'>Palestinian Territory, Occupied</option><option value='PA'>Panama</option><option value='PG'>Papua New Guinea</option><option value='PY'>Paraguay</option><option value='PE'>Peru</option><option value='PH'>Philippines</option><option value='PN'>Pitcairn</option><option value='PL'>Poland</option><option value='PT'>Portugal</option><option value='QA'>Qatar</option><option value='RE'>Reunion</option><option value='RO'>Romania</option><option value='RU'>Russian Federation</option><option value='RW'>Rwanda</option><option value='BL'>Saint Barthelemy</option><option value='SH'>Saint Helena</option><option value='KN'>Saint Kitts and Nevis</option><option value='LC'>Saint Lucia</option><option value='MF'>Saint Martin (French Part)</option><option value='PM'>Saint Pierre and Miquelon</option><option value='VC'>Saint Vincent and the Grenadines</option><option value='WS'>Samoa</option><option value='SM'>San Marino</option><option value='ST'>Sao Tome and Principe</option><option value='SA'>Saudi Arabia</option><option value='SN'>Senegal</option><option value='RS'>Serbia</option><option value='SC'>Seychelles</option><option value='SL'>Sierra Leone</option><option value='SG'>Singapore</option><option value='SX'>Sint Maarten</option><option value='SK'>Slovakia</option><option value='SI'>Slovenia</option><option value='SB'>Solomon Islands</option><option value='SO'>Somalia</option><option value='ZA'>South Africa</option><option value='GS'>South Georgia and South Sandwich Islands</option><option value='KR'>South Korea</option><option value='SS'>South Sudan</option><option value='ES'>Spain</option><option value='LK'>Sri Lanka</option><option value='SD'>Sudan</option><option value='SR'>Suriname</option><option value='SJ'>Svalbard and Jan Mayen</option><option value='SZ'>Swaziland</option><option value='SE'>Sweden</option><option value='CH'>Switzerland</option><option value='SY'>Syrian Arab Republic</option><option value='TW'>Taiwan</option><option value='TJ'>Tajikistan</option><option value='TZ'>Tanzania</option><option value='TH'>Thailand</option><option value='TL'>Timor - Leste</option><option value='TG'>Togo</option><option value='TK'>Tokelau</option><option value='TO'>Tonga</option><option value='TT'>Trinidad and Tobago</option><option value='TN'>Tunisia</option><option value='TR'>Turkey</option><option value='TM'>Turkmenistan</option><option value='TC'>Turks and Caicos Islands</option><option value='TV'>Tuvalu</option><option value='UG'>Uganda</option><option value='UA'>Ukraine</option><option value='AE'>United Arab Emirates</option><option value='GB'>United Kingdom</option><option value='UY'>Uruguay</option><option value='UM'>US Minor Outlying Islands</option><option value='UZ'>Uzbekistan</option><option value='VU'>Vanuata</option><option value='VE'>Venezuela</option><option value='VN'>Vietnam</option><option value='WF'>Wallis and Futuna</option><option value='EH'>Western Sahara</option><option value='YE'>Yemen</option><optionvalue='ZM'>Zambia</option><option value='ZW'>Zimbabwe</option>";
                break;
            }
            $('#val' + thisID + 'IDcountry').html(options);
            $('#val' + thisID + 'IDcountry').trigger('change');
        },
        isQ1: false,
        isIE: false,
        isMetro: false,
        isTouch: false,
        // THIS ONE IS TEMPORARY! AS NEEDED FOR BUG-SHOOTING ON TABLET!
        troubleshootTablet: function() {
            // place tracing/logging functions in here in order to help diagnose probs on tablet only
            setTimeout(function() {
                //console.log('width='+$(window).width());
                //console.log('height='+$(window).height());
                //console.log('isTouch='+svpApp.util.isTouch);
                //console.log('isIE='+svpApp.util.isIE);
            }, 1000);


        }
    }, // end svpApp.util
    tabs: {
        init: function() {
            //spf - 07/08/2014 - SPICE-399 - handle initialization of new tab
            //      and set both top and bottom tabs gold for Private Banker inactive tab
            var privateTab = $('ul.nav-tabs li').hasClass('privbnkcust');
            if (privateTab) {
                if ($('li.privbnkcust').hasClass('active')) {
                    $('.tab-content').addClass('private-bank');
                    $('li.privbnkcust').removeClass('privbnk');
                } else {
                    $('.tab-content').removeClass('private-bank');
                    $('li.privbnkcust').addClass('privbnk');
                };
            };

        },

        setTabs: function(thatTab) {
            var thisTab = thatTab;
            var thisPanel = $(thisTab).attr('href');
            var thisIndex = $(thisTab).parent('li').index();
            if ($(thisTab).parent().parent('ul').hasClass('nav-tabs-bottom')) {
                //spf - 06-27-2014 - BUG SPICE-242 - Don't change state of tabs if the '+' tab (search, create customer) is clicked
                if ($(thisTab).attr('data-title') !== 'Customer Search') {
                    $(thisTab).parent().parent().prev('div.tab-content').prev('ul.nav-tabs').children('li').removeClass('active');
                    $(thisTab).parent().parent().prev('div.tab-content').prev('ul.nav-tabs').children('li:eq(' + thisIndex + ')').addClass('active');
                };
            } else {
                //spf - 06-30-2014 - BUG SPICE-242 - Don't change state of tabs if the '+' tab (search, create customer) is clicked
                if ($(thisTab).attr('data-title') !== 'Customer Search') {
                    $(thisTab).parent().parent().next('div.tab-content').next('ul.nav-tabs-bottom').children('li').removeClass('active');
                    $(thisTab).parent().parent().next('div.tab-content').next('ul.nav-tabs-bottom').children('li:eq(' + thisIndex + ')').addClass('active');
                };
            };

            //spf - 05/20/2014 - handle Private Banker settings here - moved from embedded inside pages
            var thisListItem = $(thisTab).closest('li');
            if ($(thisListItem).hasClass('privbnkcust') && $(thisTab).attr('data-title') !== 'Customer Search') {
                $('.tab-content').addClass('private-bank');
                $('li.privbnkcust').removeClass('privbnk');
            } else {
                if ($(thisTab).attr('data-title') !== 'Customer Search') {
                    $(thisTab).children('a').trigger('click');
                    $('.tab-content').removeClass('private-bank');
                    $('li.privbnkcust').addClass('privbnk');
                };
            };

            svpApp.util.phoneMask();

        },
    },

    validation: {
        init: function() {

        }
    },
    // START page-specific function blocks
    dashboard: {
        init: function() {
            // init custom scrollbars
            if (!(svpApp.util.isIE > 9) || !(svpApp.util.isMetro)) {
                $('.scrollcontent').customScrollbar();
            }

            //			svpApp.util.expandToggle();
            svpApp.util.phoneMask();
            svpApp.util.dateMask();
            svpApp.util.zipMask();
            svpApp.dashboard.openCurrentSearchAccord();
            svpApp.dashboard.clearOtherAccords2();

            // trial for CLIENT SIDE VALIDATION using jqBootstrapValidation
            $("input, select, textarea").not("[type=submit]").jqBootstrapValidation({
                submitError: function($form, event, errors) {
                    svpApp.util.buildValErrors();
                    $('.alert.alert-danger').parent('div.row').slideDown();
                    $('div.panel-collapse.in dd.valerror').first().find('input').focus();
                },
                hasErrors: function() {
                    var errorMessages = [];
                    this.each(function(i, el) {
                        errorMessages = errorMessages.concat(
                            $(el).triggerHandler("getValidators.validation") ? $(el).triggerHandler("validation.validation", { submitting: true }) : []
                        );
                        $(el).trigger("submit.validation");
                    });
                    return (errorMessages.length > 0);
                    svpApp.util.buildValErrors();
                },
                submitSuccess: function($form, event) {
                    svpApp.dashboard.clearOtherAccords();
                }
            });

            $('input.valerror, input.valwarning, span.select.valerror, span.select.valwarning').on('focus', function() {
                $(this).on('blur', function() {
                    $(this).removeClass('valerror');
                    $(this).removeClass('valwarning');
                    if ($('.valerror, .valwarning').length < 1) {
                        $('div.alert.alert-danger:eq(0)').slideUp();
                    }
                    $('img.valarrow').remove();
                });
            });


        },
        clearOtherAccords: function() {
            $('.panel .panel-collapse').not('.in').find('input[type=text]').each(function() {
                $(this).val('');
            });
            $('.panel .panel-collapse').not('.in').find('select').each(function() {
                //$(this).val('Select');
                //spf - 02/24/2014 - fix this value for Select dropdown to '0' NOT 'Select'
                //      once we start using jqBootstrap Validation this would have been a bug
                $(this).val('');
            });
        },
        clearOtherAccords2: function() {
            // specifically to clear inputs/selects in CLOSED accordions ON accordion change
            $('#accordion').on('shown.bs.collapse', function() {
                $('.panel input[type=text]').val('');
                //spf - 02/24/2014 - Select dropdown value CHANGED TO '0' - NOT 'Select'
                //spf - 03/13/2014 - changed value from '0' to '' - caused problems on server in TEST
                //$('.panel select.select').val('Select').trigger('change');
                $('.panel select.select').val('').trigger('change');
                if ($('.valerror, .valwarning').length < 1) {
                    $('div.alert.alert-danger:eq(0)').slideUp();
                }
            });
        },
        openCurrentSearchAccord: function() {
            if (typeof accordSearchSection !== 'undefined') {
                if (accordSearchSection == "Name") {
                    $('#individual').addClass('in');
                    $('a[href="#individual"]').removeClass('collapsed');
                } else if (accordSearchSection == "BusinessName") {
                    $('#nonindividual').addClass('in');
                    $('a[href="#nonindividual"]').removeClass('collapsed');
                } else if (accordSearchSection == "ECN") {
                    $('#ecnOnly').addClass('in');
                    $('a[href="#ecnOnly"]').removeClass('collapsed');
                } else {
                    $('#acctOnly').addClass('in');
                    $('a[href="#acctOnly"]').removeClass('collapsed');
                }
            }
        }
    },
    customerRecord: {
        init: function() {
            if (!(svpApp.util.isMetro) || !(svpApp.util.isIE)) {
                $('.scrollcontent').customScrollbar();
            }
            svpApp.util.tinToggle.init();
            //			svpApp.util.expandToggle();
            svpApp.util.toolTips.init();
            svpApp.util.altAccordListener();
            svpApp.util.charCounter();
            svpApp.util.enterAddress();
            svpApp.util.verifyAddress();
            svpApp.util.ssnMask();
            svpApp.util.dateMask();
            svpApp.util.phoneMask();
            svpApp.util.employmentInfo();
            svpApp.util.custRecID();
            svpApp.util.expandAll();
            //svpApp.util.datePicker();
        }
    },
    requiredCustomer: {
        init: function() {
            $("#classicsvp").attr('data-classic', 'off');
            if (!(svpApp.util.isMetro) || !(svpApp.util.isIE)) {
                $('.scrollcontent').customScrollbar();
            }
            //			svpApp.util.expandToggle();
            svpApp.util.altAccordListener();
            svpApp.util.charCounter();
            svpApp.util.enterAddress();
            svpApp.util.verifyAddress();
            svpApp.util.ssnMask();
            svpApp.util.phoneMask();
            svpApp.util.dateMask();
            svpApp.util.MMYYYYMask();
            svpApp.util.employmentInfo();
            svpApp.util.expandAll();
            //svpApp.util.datePicker();
        }
    },
    productSelection: {
        init: function() {
            $("#classicsvp").attr('data-classic', 'off');

            // mark each row in the accordion if its an offer or product
            $('div.accordion table.checkboxtable tr').each(function() {
                if ($(this).hasClass('depacctrel')) {
                    // do nothing
                } else {
                    if ($(this).find('select.step1').length) {
                        $(this).addClass('hasoffer');
                    } else {
                        $(this).addClass('hasproduct');
                    }
                }
            });

            // show the products already selected and count the products and offers
            svpApp.productSelection.updateCounter();

            // checkbox listener
            $('table.checkboxtable td.col1 input[type=checkbox]').on('click', function() {
                if ($('table.checkboxtable.notintab').length > 0) {
                    var thisPanel = '#notintab';
                } else {
                    var thisPanel = '#' + ($('.tab-pane.active').attr('id'));
                }
                if ($(this).closest('.panel-group').length > 0) {
                    // only for accordions
                    var thisMany = 0;
                    var existingChecks = $(this).closest('table.checkboxtable').find('td.col1 input[type=checkbox]:checked').length;
                    var thisIdx = $(this).closest('.panel').index();

                    if ($(this).prop('checked')) {
                        if ($(this).closest('table.checkboxtable').hasClass('accountssection')) {
                            $(this).parent().next('td').find('dl.subdl').slideDown();
                            svpApp.productSelection.updateCounter();
                        } else {
                            $(this).parent().next('td').find('dl.subdl').slideDown();
                            svpApp.productSelection.updateCounter();
                        }
                    } else {
                        $(this).parent().next('td').find('dl.subdl').slideUp();
                        $(this).parent().next('td').find('div.step2.row').slideUp();
                        svpApp.productSelection.updateCounter();
                        if (existingChecks < 1) {
                            $(this).closest('.panel').find('span.number-selected').fadeOut();
                        }
                        if ($(this).closest('table').hasClass('accountssection')) {
                            //$(this).parent().parent().next('tr').slideUp();
                        }
                    }
                } else {
                    if ($(this).prop('checked')) {
                        $(this).parent().next('td').find('dl.subdl').slideDown();
                    } else {
                        $(this).parent().next('td').find('dl.subdl').slideUp();
                    }
                }
                if (thisPanel == '#notintab') {
                    svpApp.productSelection.accordListenersNoTab();
                } else {
                    svpApp.productSelection.accordListeners(thisPanel);
                }
            });

            //			svpApp.util.expandToggle();
            svpApp.util.phoneMask();
            svpApp.util.altAccordListener();

            //			svpApp.productSelection.updateProdOffCount('#' + ($('.tab-pane.active').attr('id')));

            //svpApp.util.expandAll();


            return this;
        }, // end productSelection.init
        updateCounter: function() {
            $('.accordion .panel').each(function(i) {
                var thisEl = $(this).find('.panel-heading h4 span.number-selected');
                var thisElProd = $(this).find('.panel-heading .span.pull-right span.productcount');
                var thisElOffr = $(this).find('.panel-heading span.pull-right span.offercount');
                var howMany = $('.accordion .panel:eq(' + i + ') table.checkboxtable td.col1 input:checked').length;
                var howManyProd = $('.accordion .panel:eq(' + i + ') table.checkboxtable tr.hasproduct').length;
                var howManyOffr = $('.accordion .panel:eq(' + i + ') table.checkboxtable tr.hasoffer').length;
                if (howManyProd === 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.productcount').html(howManyProd + ' Product');
                } else if (howManyProd < 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.productcount').html(howManyProd + ' Products');
                } else if (howManyProd > 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.productcount').html(howManyProd + ' Products');
                }
                if (howManyOffr === 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.offercount').html(howManyOffr + ' Offer');
                } else if (howManyOffr < 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.offercount').html('No Offers');
                } else if (howManyOffr > 1) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right span.offercount').html(howManyOffr + ' Offers');
                }

                if ((howManyOffr == 0) && (howManyProd == 0)) {
                    $(' div.accordion .panel:eq(' + i + ') .panel-heading span.pull-right').hide();
                }

                if (howMany === 1) {
                    $(thisEl).find('i').html(howMany);
                    $(thisEl).find('b').hide();
                    $(thisEl).show();
                } else if (howMany > 1) {
                    $(thisEl).find('i').html(howMany);
                    $(thisEl).find('b').show();
                    $(thisEl).show();
                } else if (howMany < 1) {
                    $(thisEl).find('i').html(howMany);
                    $(thisEl).find('b').show();
                    $(thisEl).hide();
                }
            });
            return this;
        },
        accordListeners: function(thisPanel) {
            // just for accordion subDL dropdowns
            $(thisPanel + ' div.accordion dl.subdl dd select.step1').on('change', function() {
                if ($(this).val() === '0') {
                    $(this).closest('dl.subdl').next('div.step2').slideUp();
                } else {
                    $(this).closest('dl.subdl').next('div.step2').slideDown();
                }
            });
            // for the 'step3' dropdowns in ACCOUNTS section specifically (show a new Account row)
            $(thisPanel + ' div.accordion table.accountssection dl.subsubdl dd select.step3').on('change', function() {
                if ($(this).val() === '0') {
                    $(this).closest('tr').next('tr').slideUp();
                } else {
                    $(this).closest('tr').next('tr').slideDown();
                }
            });
            svpApp.util.altAccordListener();
            return this;
        },
        accordListenersNoTab: function() {
            // just for accordion subDL dropdowns
            $('div.accordion dl.subdl dd select.prodacct').on('change', function() {
                if ($(this).val() === '0') {
                    $('tr.depacctrel').slideUp();
                    $(this).closest('tr').next('tr').slideUp();
                } else {
                    $('tr.depacctrel').slideDown();
                    $(this).closest('tr').next('tr').slideDown();
                }
            });
            // for the 'step3' dropdowns in ACCOUNTS section specifically (show a new Account row)
            $('div.accordion table.accountssection dl.subsubdl dd select.step3').on('change', function() {
                if ($(this).val() === '0') {
                    $(this).closest('tr').next('tr').slideUp();
                } else {
                    $(this).closest('tr').next('tr').slideDown();
                }
            });
            svpApp.util.altAccordListener();
            return this;
        },
        updateProdOffCount: function(thisPanel) {
            $('div.accordion .panel').each(function() {
                var thisIdx = $(this).index();
                var prodCount = $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') tr.hasproduct').length;
                var offerCount = $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') tr.hasoffer').length;
                if (prodCount === 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.productcount').html(prodCount + ' Product');
                } else if (prodCount < 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.productcount').html(prodCount + ' Products');
                } else if (prodCount > 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.productcount').html(prodCount + ' Products');
                }
                if (offerCount === 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.offercount').html(offerCount + ' Offer');
                } else if (offerCount < 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.offercount').html('No Offers');
                } else if (offerCount > 1) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right span.offercount').html(offerCount + ' Offers');
                }

                if ((offerCount == 0) && (prodCount == 0)) {
                    $(thisPanel + ' div.accordion .panel:eq(' + thisIdx + ') .panel-heading span.pull-right').hide();
                }
            });
            return this;
        },
        tabReInit: function(thisPanel) {
            svpApp.productSelection.updateProdOffCount(thisPanel);
            svpApp.productSelection.accordListeners(thisPanel);
            svpApp.productSelection.checkOtherTabChecks();
            svpApp.productSelection.updateCounter();
            return this;
        },
        checkOtherTabChecks: function() {
            // for the 'topthree' table
            $('div.tab-pane:not(.active) table.checkboxtable.topthree td.col1 input:checked').each(function(i) {
                var thisRow = $(this).closest('tr').index();
                //var thisPanel = $(this).closest('div.panel').index();
                var thisName = $(this).closest('.tab-pane').attr('id');
                $('div.tab-pane.active table.checkboxtable.topthree tr:eq(' + thisRow + ') td.col1 input[type=checkbox]').prop('checked', true).parent().next('td').find('div.statustext').show().find('i').html(thisName);
            });

            /*  REMOVING  - do not want inactive tab checkboxes updated based on active tab on product opportunities
                        // for the 'accordion' checkbox table
                        $('div.tab-pane:not(.active) div.panel table.checkboxtable td.col1 input:checked').each( function(i){
                            var thisRow = $(this).closest('tr').index();
                            var thisPanel = $(this).closest('div.panel').index();
                            var thisName = $(this).closest('.tab-pane').attr('id');
                            $('div.tab-pane.active div.panel:eq('+thisPanel+') table.checkboxtable tr:eq('+thisRow+') td.col1 input[type=checkbox]').prop('checked',true).parent().next('td').find('div.statustext').show().find('i').html(thisName);
                        });     */
            return this;
        }
    }, // end productSelection
    productAcceptance: {
        init: function() {
            svpApp.productAcceptance.setupCounters();

            // listen for tab clicks
            $('ul.nav-tabs li a').on('click', function() {
                var thisPanel = $(this).attr('href');
                setTimeout(function() {
                    svpApp.productAcceptance.tabReInit();
                }, 150);
                var thisIndex = $(this).parent('li').index();
                if ($(this).parent().parent('ul').hasClass('nav-tabs-bottom')) {
                    $(this).parent().parent().prev('div.tab-content').prev('ul.nav-tabs').children('li').removeClass('active');
                    $(this).parent().parent().prev('div.tab-content').prev('ul.nav-tabs').children('li:eq(' + thisIndex + ')').addClass('active');
                } else {
                    $(this).parent().parent().next('div.tab-content').next('ul.nav-tabs-bottom').children('li').removeClass('active');
                    $(this).parent().parent().next('div.tab-content').next('ul.nav-tabs-bottom').children('li:eq(' + thisIndex + ')').addClass('active');
                }
            });

            // listen for changes to select boxes (to mark the ones in the other tab as 'joint account accepted')
            $('select.select').on('change', function() {
                var thisRow = $(this).closest('tr').attr('data-type'); // eg: product-advantagechecking
                var thisName = $('.tab-pane.active').attr('id');
                var thatName = $('.tab-pane:not(".active")').attr('id'); // eg: Nate
                var thatPanel = '#' + thatName;
                if ($(this).val() != 0) {
                    $(thatPanel + ' table.prodsandoffers tr[data-type="' + thisRow + '"] > td.col2 > dl > dd > div.selectContainer select.select').hide().next('span.select').hide().parent().after('<span class="redtext font12 bolded">Joint account accepted by <i>' + thisName + '</i></span>');
                } else {
                    $(thatPanel + ' table.prodsandoffers tr[data-type="' + thisRow + '"] > td.col2 > dl > dd > div.selectContainer select.select').show().next('span.select').show().parent().next('span.redtext.font12.bolded').remove();
                }
            });
        },
        setupCounters: function() {
            var offcounter = 0, prodcounter = 0, thisPanel = '#' + ($('div.tab-content div.tab-pane.active').attr('id'));
            $('body#product_acceptance ' + thisPanel + ' table.prodsandoffers td.col1 span.graytext.allcaps:contains("Offer")').each(function() {
                $(this).closest('tr').addClass('hasoffer');
            });
            $('body#product_acceptance ' + thisPanel + ' table.prodsandoffers td.col1 span.graytext.allcaps:contains("Product")').each(function() {
                $(this).closest('tr').addClass('hasproduct');
            });
            offcounter = $(thisPanel + ' table.prodsandoffers tr.hasoffer').length;
            prodcounter = $(thisPanel + ' table.prodsandoffers tr.hasproduct').length;
            var $offEl = $(thisPanel + '.tab-pane.active h3.topcount span.pull-right span.Accept_offerscount');
            var $prodEl = $(thisPanel + '.tab-pane.active h3.topcount span.pull-right span.Accept_prodscount');
            $offEl.find('i').html(offcounter);
            $prodEl.find('i').html(prodcounter);
            if (offcounter > 1) {
                $offEl.find('b').show();
            } else if (offcounter == 1) {
                $offEl.find('b').hide();
            } else if (offcounter < 1) {
                $offEl.find('b').show();
            }
            if (prodcounter > 1) {
                $prodEl.find('b').show();
            } else if (prodcounter == 1) {
                $prodEl.find('b').hide();
            } else if (prodcounter < 1) {
                $prodEl.find('b').show();
            }
        },
        tabReInit: function() {
            svpApp.productAcceptance.setupCounters();
        }
    },
    productFulfillment: {
        init: function() {
            $("#classicsvp").attr('data-classic', 'off');
            // for 'other' address block - use #TARGET id in 'data-toggle' attr
            $('select.expand-toggle').change(function() {
                var thisLink = $(this).attr('data-toggle');
                if ($(this).val() != 0) {
                    $(thisLink).slideDown();
                } else {
                    $(thisLink).slideUp();
                }
            });

            //			svpApp.util.expandToggle();
            svpApp.util.charCounter();
            svpApp.util.phoneMask();
            svpApp.util.notEmpty();
            svpApp.util.altAccordListener();
            svpApp.util.enterAddress();
            svpApp.util.verifyAddress();
        }
    },
    accountDetail: {
        init: function() {
            //svpApp.util.plusMinusToggle();
            svpApp.util.tinToggle.init();
            /*			not using this 'ajax' loading in AccountDetail anymore
                        $('#account_detail div.statusblocks a').on('click', function(e){
                            var $onpage = $('.acct-details');
                            var thisHref = $(this).attr('href');
                            var $ajaxContainer = $('#account_detail #ajaxContainer');
                            if ($(this).hasClass('open')){
                                //$(this).removeClass('open');
                                // do nothing
                            } else {
                                $('div.statusblocks a.open').removeClass('open');
                                $(this).addClass('open');
                                $ajaxContainer.slideUp(400, function(){
                                    if (thisHref.indexOf('.html') > 0){
                                        $onpage.slideUp();
                                        $ajaxContainer.load(thisHref, function(){
                                            $('img#loading').fadeOut(1200);
                                            $ajaxContainer.slideDown(600);
                                            svpApp.util.expandToggle();
                                        });
                                    } else {
                                        $(this).addClass('open');
                                        $('#ajaxContainer').slideUp();
                                        $onpage.slideDown(600);
                                    }
                                });
                                $(thisHref).slideDown(600);
                            }
                            e.preventDefault();
                            return false;
                        }); */
            if (!(svpApp.util.isMetro) || !(svpApp.util.isIE)) {
                $('.scrollcontent').customScrollbar();
            }

        }
    },
    searchResults: {
        init: function() {
            svpApp.util.tableSelectable();
        }
    },
    prodConfirm: {
        init: function() {
            $("#classicsvp").attr('data-classic', 'off');
        }
    },
    customerSnapshot: {
        init: function() {
            svpApp.util.expandAllnotab();
            svpApp.util.altAccordListener();
        }
    },
    customerRecommendations: {
        init: function() {
            svpApp.util.expandAllnotab();
            svpApp.util.altAccordListener();
            svpApp.util.textTruncateListener();
            //svpApp.productSelection.init();
            /*
			$('.panel-group .panel div.text-truncate').each(function(){
				var thisIdx = $(this).closest('.panel').index();
				svpApp.productSelection.initEllipsis(thisIdx);
				svpApp.productSelection.initEllipsisClicks();
			});
			*/
        }
    }
}; // end svpApp
var onPrintFinished = function(printed) {
    $(".wmgheader").show();
    $(".btn").show();
    $("#collapse").show();
    $(".summaryRow a").css("visibility", "");
}; //print command

function printOut() {

    $(".wmgheader").hide();
    $(".btn").hide();
    $("#collapse").hide();
    $(".summaryRow a").css("visibility", "hidden");

    //window.print() stops JS execution
    setTimeout(function() {
        try {
            onPrintFinished(window.print());
        } catch (ex) {
            onPrintFinished();


        }

    }, 1000);

    //Remove div once printed

}