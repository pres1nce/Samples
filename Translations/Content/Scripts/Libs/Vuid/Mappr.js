/*!
* Mappr v0.1
* www.vinnix.com
*
* Copyright (c) Vincent Cifelli
*/

/*
* Vuid is a Overlay Engine specializing in hot key implementation and UI 
*  Requires:jQuery,Vuid, Tipe*  
* Authors        Vincent Cifelli
* Contributors   //
*/



(function ($) {

    Mappr = function () {

    };

    Mappr.prototype.init = function () {
        console.log("mappr - alive");
    }

    Mappr.prototype.AccentCharacters =
{
    GraveCharacters:
	[
		{ keyCode: 65, Character: 'À' },
		{ keyCode: 69, Character: 'È' },
		{ keyCode: 73, Character: 'Ì' },
		{ keyCode: 79, Character: 'Ò' },
		{ keyCode: 85, Character: 'Ù' },
		{ keyCode: 97, Character: 'à' },
		{ keyCode: 101, Character: 'è' },
		{ keyCode: 105, Character: 'ì' },
		{ keyCode: 111, Character: 'ò' },
		{ keyCode: 117, Character: 'ù' },
	],

    AccentCharacters:
	[
		{ keyCode: 65, Character: 'Á' },
		{ keyCode: 69, Character: 'É' },
		{ keyCode: 73, Character: 'Í' },
		{ keyCode: 79, Character: 'Ó' },
		{ keyCode: 85, Character: 'Ú' },
		{ keyCode: 89, Character: 'Ý' },
		{ keyCode: 97, Character: 'á' },
		{ keyCode: 101, Character: 'é' },
		{ keyCode: 105, Character: 'í' },
		{ keyCode: 111, Character: 'ó' },
		{ keyCode: 117, Character: 'ú' },
		{ keyCode: 121, Character: 'ý' },
	],

    UmlautCharacters:
	[
		{ keyCode: 65, Character: 'Ä' },
		{ keyCode: 69, Character: 'Ë' },
		{ keyCode: 73, Character: 'Ï' },
		{ keyCode: 79, Character: 'Ö' },
		{ keyCode: 85, Character: 'Ü' },
		{ keyCode: 89, Character: 'Ÿ' },
		{ keyCode: 97, Character: 'ä' },
		{ keyCode: 101, Character: 'ë' },
		{ keyCode: 105, Character: 'ï' },
		{ keyCode: 111, Character: 'ö' },
		{ keyCode: 117, Character: 'ü' },
		{ keyCode: 121, Character: 'ÿ' },
	],

    CircumflexCharacters:
	[
		{ keyCode: 65, Character: 'Â' },
		{ keyCode: 69, Character: 'Ê' },
		{ keyCode: 73, Character: 'Î' },
		{ keyCode: 79, Character: 'Ô' },
		{ keyCode: 85, Character: 'Û' },
		{ keyCode: 97, Character: 'â' },
		{ keyCode: 101, Character: 'ê' },
		{ keyCode: 105, Character: 'î' },
		{ keyCode: 111, Character: 'ô' },
		{ keyCode: 117, Character: 'û' },
	],


    TildeCharacters:
	[
		{ keyCode: 65, Character: 'Ã' },
		{ keyCode: 67, Character: 'Ç' },
		{ keyCode: 78, Character: 'Ñ' },
		{ keyCode: 79, Character: 'Õ' },
		{ keyCode: 97, Character: 'ã' },
		{ keyCode: 99, Character: 'ç' },
		{ keyCode: 110, Character: 'ñ' },
		{ keyCode: 111, Character: 'õ' },
	]
};


})(jQuery);