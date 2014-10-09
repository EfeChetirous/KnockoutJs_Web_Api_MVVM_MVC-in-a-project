$(function () {

    var categoryViewModel = function () {
        var self = this;

        self.Name = ko.observable();
        self.ControllerName = ko.observable();
        self.ActionName = ko.observable();

        self.AllCategories = ko.observableArray();

        self.GetAllCategories = function getAllCategories() {
            $.getJSON("/api/Category/GetAllCategories", function (result) {
                $.each(result, function (idx, item) {
                    self.AllCategories.push(item);
                });
            });
        };

        self.fillTexts = function fillTexts(data) {
            
            self.Name(data.Name);
            self.ControllerName(data.ControllerName);
            self.ActionName(data.ActionName);
        };

        self.SendModel = function sendModel(data) {
            //var modeljson = ko.toJSON(data);
            var model = {
                Name: data.Name,
                ControllerName: data.ControllerName,
                ActionName: data.ActionName
            };

            $.post("/api/Category/SendModel", model, function (data,Status) {
                alert("data has been posted to server!");
                self.Name();
                self.ControllerName();
                self.ActionName();

                $.each(data, function (idx, item) {
                    self.AllCategories.push(item);
                });
            });
        };

        //self.GetAllCategories = $.ajax({
        //    url: 'http://localhost:1949/api/Category/GetAllCategories',
        //    type: 'GET',
        //    dataType: 'json',
        //    success: function (data, textStatus, xhr) {
        //        console.log(data);
        //    },
        //    error: function (xhr, textStatus, errorThrown) {
        //        console.log('Error in Operation');
        //    }
        //});

        self.GetAllCategories();
    };
    ko.applyBindings(new categoryViewModel());
    
});