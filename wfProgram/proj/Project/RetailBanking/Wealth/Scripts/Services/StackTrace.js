
wmg.factory(
          "stacktraceService",
          function () {

              // "printStackTrace" is a global object.
              return ({
                  print: printStackTrace
              });

          }
      );

// By default, AngularJS will catch errors and log them to
// the Console. We want to keep that behavior; however, we
// want to intercept it so that we can also log the errors
// to the server for later analysis.
wmg.provider(
    "$exceptionHandler",
    {
        $get: function (errorLogService) {

            return (errorLogService);

        }
    }
);


// -------------------------------------------------- //
// -------------------------------------------------- //


// The error log service is our wrapper around the core error
// handling ability of AngularJS. Notice that we pass off to
// the native "$log" method and then handle our additional
// server-side logging.
wmg.factory(
    "errorLogService",
    function ($log, $window, stacktraceService) {

        // I log the given error to the remote server.
        function log(exception, cause) {

            // Pass off the error to the default error handler
            // on the AngualrJS logger. This will output the
            // error to the console (and let the application
            // keep running normally for the user).
            $log.error.apply($log, arguments);

            // Now, we need to try and log the error the server.
            // --
            // NOTE: In production, I have some debouncing
            // logic here to prevent the same client from
            // logging the same error over and over again! All
            // that would do is add noise to the log.
            try {

                var errorMessage = exception.toString();
                var stackTrace = stacktraceService.print({ e: exception });

                // Log the JavaScript error to the server.
                // --
                // NOTE: In this demo, the POST URL doesn't
                // exists and will simply return a 404.



              //  var parameter = angular.copy(AppRoot);
/*                parameter.ViewModel.ErrorMessages = angular.toJson({
                    errorUrl: $window.location.href,
                    errorMessage: errorMessage,
                    stackTrace: stackTrace,
                    cause: (cause || "")
                });*/
             //   parameter.ViewModel.ErrorMessages = JSON.stringify(parameter.ViewModel.ErrorMessages);
             //   parameter.ViewModel.ErrorMessages = errorMessage;
                //parameter.ViewModel.ucaViewModel = null;
                //var rootJsonString = JSON.stringify(parameter);

                //console.log(parameter);
                //console.log(rootJsonString);
                $.post("./WealthError.aspx", { SessionID: sessionIdData, errormsg: errorMessage, access_token: accessToken }, function (data) {
                   // console.log(data);
                });
                
                /*
                $.ajax({
                    type: "POST",
                    url: "./javascript-errors",
                    contentType: "application/json",
                    data: angular.toJson({
                        errorUrl: $window.location.href,
                        errorMessage: errorMessage,
                        stackTrace: stackTrace,
                        cause: (cause || "")
                    })
                });*/

            } catch (loggingError) {

                // For Developers - log the log-failure.
                $log.warn("Error logging failed");
                $log.log(loggingError);

            }

        }


        // Return the logging function.
        return (log);

    }
);


// -------------------------------------------------- //
// -------------------------------------------------- //


// I control the root of the application.
wmg.controller(
    "AppController",
    function ($scope) {

        // ---
        // PUBLIC METHODS.
        // ---


        // I cause an error to be thrown in nested functions.
        $scope.causeError = function () {

            foo();

        };


        // ---
        // PRIVATE METHODS.
        // ---


        function bar() {

            // NOTE: "y" is undefined.
            var x = y;

        }


        function foo() {

            bar();

        }

    }
);