ko.bindingHandlers.chosen = {
    init: function (element) {
        var $element = $(element);
        var options = ko.unwrap(valueAccessor());

        if (typeof options === 'object')
            $element.chosen(options);
        else
            $element.chosen();

        ['options', 'selectedOptions', 'value'].forEach(function (propName) {
            if (allBindings.has(propName)) {
                var prop = allBindings.get(propName);
                if (ko.isObservable(prop)) {
                    prop.subscribe(function () {
                        $element.trigger('chosen:updated');
                    });
                }
            }
        });
    },
    update: function (element, valueAccessor, allBindings) {
        ko.bindingHandlers.options.update(element, valueAccessor, allBindings);
        $(element).trigger('chosen:updated');
    }
};
ko.bindingHandlers.autoNumeric = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        var settings = allBindings().autoNumericSettings;
        var observable = valueAccessor();
        $(element).val(ko.unwrap(observable));
        $(element).autoNumeric('init', settings);
        ko.utils.registerEventHandler(element, 'focusout', function () {
            var value = $(element).autoNumeric('get');
            observable(isNaN(value) ? 0 : value);
        });
    },
    update: function (element, valueAccessor, allBindings) {
        var observable = valueAccessor();
        $(element).autoNumeric('set', ko.unwrap(observable));
    }
};


ko.bindingHandlers.datepicker = {   
    init: function (element, valueAccessor) {
        var options = valueAccessor();
        $(element).parent().datepicker(options || {});
    },
    update: function (element, valueAccessor, allBindings) {
        ko.bindingHandlers.options.update(element, valueAccessor, allBindings);
    }
};