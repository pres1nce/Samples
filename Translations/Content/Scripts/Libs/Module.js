
Module = function () {

}

Module.prototype.init = function (container) {

	this.$module = $(container);

	this.attachEvents();

	this.initModule();

}

Module.prototype.$elements = {}

Module.prototype.$el = function (name, retrieveFresh) {
	if (!this.$elements[name] || retrieveFresh) {
		this.$elements[name] = $(this.elementMap[name] || name, this.$module[0]);
	}

	return this.$elements[name];
}

Module.prototype.attachEvents = function () {
	this.$module.on('click.module', '[data-clickaction]', $.proxy(this.handleAction, this));
	//this.$module.on('change.module', '[data-changeaction]', $.proxy(this.handleAction, this));
}

Module.prototype.handleAction = function (e) {

	var type = e.type,
		$el = $(e.currentTarget),
		tag = $el[0].tagName.toLowerCase(),
		action = $el.attr('data-' + type + 'action'),
		actionValue;

	if (/(select|input)/.test(tag)) {
		if ($el.attr("type") == "checkbox") {
			actionValue = $el.is(":checked") ? true : false;
		} else {
			actionValue = $el.val();
		}
	}
	else {
		actionValue = $el.data('actionvalue');
	}


	if (tag != 'input') {
		e.preventDefault();
	}

	e.stopPropagation();

	if (this['action_' + action]) {
		this['action_' + action]($el, actionValue, e);
	}
	else if (typeof Debug != 'undefined') {
		console.info(this.moduleId, this.$module, 'Module:handleAction', action, 'action not found');
	}


}

Module.prototype.initModule = function () {
	// stub - will be overloaded
}
