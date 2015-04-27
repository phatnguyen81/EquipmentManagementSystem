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
    init: function (element, valueAccessor) {
        var options = valueAccessor();
        $(element).autoNumeric(options || { });
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor) {
        var options = valueAccessor();
        $(element).parent().datepicker(options || {language: "vi"});
    },
    update: function (element, valueAccessor, allBindings) {
        ko.bindingHandlers.options.update(element, valueAccessor, allBindings);
    }
};