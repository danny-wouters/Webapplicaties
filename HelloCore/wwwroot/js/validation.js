// https://stackoverflow.com/questions/48066208/mvc-jquery-validation-does-not-accept-comma-as-decimal-separator
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)?(?:,\d+)?$/.test(value);
}