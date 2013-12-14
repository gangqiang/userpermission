/*!
* lhgcore Dialog Plugin v3.1.3
* Date : 2010-06-12 15:09:11
* Copyright (c) 2009 - 2010 By Li Hui Gang
*/

; (function($) {
    $.ui = window.ui || {};
    var top = window, cover, doc, dragDiv, ZIndex, dcount = 0;
    while (top.parent != top)
        top = top.parent;
    doc = top.document;
    function getsrc() {
        if ($.browser.msie)
            return ($.browser.version == "7.0 " ? '' : "javascript:''");
        else
            return 'javascript:void(0);';
    };

    function getZIndex() {
        if (!ZIndex) ZIndex = 299999;
        return ++ZIndex;
    };

    function reSizeHdl() {
        var rel = $.ui.root(doc);
        $(cover).css({
            width: Math.max(rel.scrollWidth, rel.clientWidth || 0) - 1 + 'px',
            height: Math.max(rel.scrollHeight, rel.clientHeight || 0) - 1 + 'px'
        });
    };

    $.fn.fixie6png = function() {
        var els = $('*',this), iebg, bgIMG;
        for (var i = 0, l = els.length; i < l; i++) {
            bgIMG = $(els[i]).css('backgroundImage');
            if (bgIMG.indexOf('.png') !== -1) {
                iebg = bgIMG.replace(/url\(|"|\)/g, '');
                els[i].style.backgroundImage = 'none';
                els[i].runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + iebg + "',sizingMethod='scale')";
            }
        }
    };
    $.ui.root = function(a) {
        return a.compatMode === "CSS1Compat" ? a.documentElement : a.body;
    }
    $.ui.getScrollSize = function(a) {
        a = a || window;
        if ('pageXOffset' in a) {
            return {
                x: a.pageXOffset || 0,
                y: a.pageYOffset || 0
            };
        }
        else {
            var doc = $.ui.root(a.document);
            return {
                x: doc.scrollLeft || 0,
                y: doc.scrollTop || 0
            };
        }
    };

    $.ui.getClientSize = function(a) {
        a = a || window;
        var doc = $.ui.root(a.document);
        return {
            w: doc.clientWidth || 0,
            h: doc.clientHeight || 0
        }
    };

    function cleanDiv() {
        $(dragDiv).remove();
        dragDiv = null;
        if (cover) {
            $(cover).remove();
            cover = null;
        }
    };

    $.fn.dialog = function(opts) {
        var dialog = false;
        if (this[0])
            dialog = new $.ui.dialog(opts, this[0]);
        return dialog;
    };

    $.ui.dialog = function(opts, elem) {
        var self = this, r = this.opt =
	$.extend({
	    height: 300,
	    width: 400,
	    id: 'lhgdlgId',
	    event: 'click',
	    link: false,
	    btns: true,
	    fixed: false,
	    drag: true,
	    rang: true,
	    resize: true,
	    top: 'center',
	    left: 'center',
	    title: 'lhgdialog',
	    regDragWindow: []
	}, opts || {});

        if (r.SetTopWindow) {
            top = r.SetTopWindow;
            doc = top.document;
        }

        if (dcount === 0) {
            if ($.browser.msie && !($.browser.version == "7.0")) {
                try {
                    doc.execCommand('BackgroundImageCache', false, true);
                } catch (e) { }
            }
            dcount += 1;
        }

        iframe = $.browser.msie && !($.browser.version == "7.0" )? '<iframe hideFocus="true" ' +
	    'frameborder="0" src="' + getsrc() + '" style="position:absolute;' +
		'z-index:-1;width:100%;height:100%;top:0px;left:0px;filter:' +
		'progid:DXImageTransform.Microsoft.Alpha(opacity=0)"><\/iframe>' : '';

        if (!dragDiv) {
            dragDiv = $('<div id="lhgdig_cDiv" style="position:absolute;top:0px;left:0px;' +
		    'border:1px solid #000;background-color:#999;display:none;"></div>', doc)
			.css('opacity', 0.3).appendTo('body').bind('contextmenu', function(ev) { ev.preventDefault(); })[0];
        }

        this.SetIFramePage = function() {
            var innerDoc, text;

            if (r.html) {
                if (typeof r.html === 'string')
                    innerDoc = '<div id="lhgdig_inbox" class="lhgdig_inbox" style="display:none">' + r.html + '</div>';
                else
                    innerDoc = '<div id="lhgdig_inbox" class="lhgdig_inbox" style="display:none"></div>';
            }
            else if (r.page) {
                innerDoc = ['<iframe frameborder="0" src="', r.page, '" scrolling="auto" ',
				'id="lhgfrm" style="display:none;width:100%;height:100%;"><\/iframe>'].join('');
            }

            text = [
			'<div id="', r.id, '" class="lhgdig" style="width:', r.width, 'px;height:', r.height, 'px;">',
				'<table border="0" cellspacing="0" cellpadding="0">',
				'<tr>',
					'<td class="lhgdig_leftTop"></td>',
					'<td id="lhgdig_drag" class="lhgdig_top">',
						'<div class="lhgdig_title"><span id="lhgdig_icon" class="lhgdig_icon"></span>', r.title, '</div>',
						'<div id="lhgdig_xbtn" class="lhgdig_xbtn"></div>',
					'</td>',
					'<td class="lhgdig_rightTop"></td>',
				'</tr>',
				'<tr>',
					'<td class="lhgdig_left" id="lhgdigLeft"></td>',
					'<td>',
						'<table border="0" cellspacing="0" cellpadding="0">',
						'<tr>',
							'<td id="lhgdig_content" class="lhgdig_content">',
								innerDoc, '<div id="throbber" class="lhgdig_throbber"><span id="lhgdig_load">loading...</span></div>',
							'</td>',
						'</tr>',
						r.btns ? '<tr><td id="lhgdig_btns" class="lhgdig_btns"><div id="lhgdig_bDiv" class="lhgdig_bDiv"></div></td></tr>' : '',
						'</table>',
					'</td>',
					'<td class="lhgdig_right"></td>',
				'</tr>',
				'<tr>',
					'<td class="lhgdig_leftBottom"></td>',
					'<td class="lhgdig_bottom"></td>',
					'<td id="lhgdig_drop" class="lhgdig_rightBottom"></td>',
				'</tr>',
				'</table>', iframe,
			'</div>'
		].join('');

            return text;
        };

        this.ShowDialog = function() {

            if ($('#' + r.id, doc)[0])
                return;
            if (r.cover)
                this.ShowCover();
            var fixpos = r.fixed && (!$.browser.msie || ($.browser.version == "7.0")) ? 'fixed' : 'absolute',
            html = this.SetIFramePage();
            this.dlg = $(html, doc).css({
                position: fixpos, zIndex: getZIndex()
            }).appendTo(doc.body)[0];

            this.iPos(this.dlg, r.top, r.left, r.fixed);
            this.setDialog(this.dlg);
            if (r.drag)
                this.initDrag($('#lhgdig_drag', this.dlg)[0]);
            if (r.resize)
                this.initSize($('#lhgdig_drop', this.dlg)[0]);
            if ($.browser.msie && !($.browser.version == "7.0")) {
                var ie6PngRepair = $('html', doc).css('ie6PngRepair') === 'true' ? true : false;
                if (ie6PngRepair) {
                    $(this.dlg).fixie6png();
                }
            }

            this.lhgDigxW = $('#lhgdigLeft', this.dlg)[0].offsetWidth * 2;
            this.reContentSize(this.dlg);
            if (r.html && r.cusfn) r.cusfn();
            if (r.html) {
                $('#throbber', this.dlg).css('display', 'none');
                $('#lhgdig_inbox', this.dlg)[0].style.display = 'inline-block';
            }
        };

        this.iPos = function(dig, tp, lt, fix) {
            var cS = $.ui.getClientSize(top),
		    sS = $.ui.getScrollSize(top),
			dW = dig.offsetWidth,
			dH = dig.offsetHeight, x, y;
            if (fix) {
                if ($.browser.msie && !($.browser.version == "7.0")) {
                    $('html', doc).addClass('lhgdig_ie6_fixed');
                    $('<div class="lhgdig_warp"></div>', doc).appendTo(doc.body).append(dig).css('zIndex', getZIndex());
                }

                lx = 0;
                rx = cS.w - dW;
                cx = (rx - 20) / 2;

                ty = 0;
                by = cS.h - dH;
                cy = (by - 20) / 2;
            }
            else {
                lx = sS.x;
                cx = sS.x + (cS.w - dW - 20) / 2;
                rx = sS.x + cS.w - dW;

                ty = sS.y;
                cy = sS.y + (cS.h - dH - 20) / 2;
                by = sS.y + cS.h - dH;
            }

            switch (lt) {
                case 'center':
                    x = cx;
                    break;
                case 'left':
                    x = lx;
                    break;
                case 'right':
                    x = rx;
                    break;
                default:
                    if (fix) lt = lt - sS.x;
                    x = lt; break;
            }

            switch (tp) {
                case 'center':
                    y = cy;
                    break;
                case 'top':
                    y = ty;
                    break;
                case 'bottom':
                    y = by;
                    break;
                default:
                    if (fix) tp = tp - sS.y;
                    y = tp; break;
            }

            $(dig).css({ top: y + 'px', left: x + 'px' });
        };

        this.setDialog = function(dlg) {
            this.win = window;
            this.top = top;
            $(dlg).bind('contextmenu', function(ev) {
                ev.preventDefault();
            }).bind('mousedown', self.setIndex);

            $('#lhgdig_xbtn', dlg).hover(function() {
                $(this).addClass('lhgdig_xbtnover');
            }, function() {
                $(this).removeClass('lhgdig_xbtnover');
            }).click(self.cancel);

            if (r.html && r.html.nodeType)
                $('#lhgdig_inbox', dlg).append(r.html);

            this.regWindow = [window];

            if (r.regDragWindow.length > 0)
                this.regWindow.push(r.regDragWindow);

            if (top != window)
                this.regWindow.push(top);

            if (r.page) {
                this.infrm = $('#lhgfrm', dlg)[0];
                if (!r.link) {
                    this.inwin = this.infrm.contentWindow;
                    this.infrm.dg = this;
                }

                $(this.infrm).bind('load', function() {
                    if (!self.opt.link) {
                        var indw = $.browser.msie ? this.contentWindow.document : this.contentWindow;
                        $(indw).bind('mousedown', self.setIndex);
                        self.regWindow.push(this.contentWindow);
                    }

                    $('#throbber', self.dlg)[0].style.display = 'none';
                    this.style.display = 'block';
                });
            }
        };

        this.reContentSize = function(dig) {
            var tH = $('#lhgdig_drag', dig)[0].offsetHeight,
		    bH = $('#lhgdig_drop', dig)[0].offsetHeight,
			xW = this.lhgDigxW,
			nH = r.btns ? $('#lhgdig_btns', dig)[0].offsetHeight : 0,
			iW = parseInt(dig.style.width, 10) - xW,
			iH = parseInt(dig.style.height, 10) - tH - bH - nH;

            $('#lhgdig_content', dig).css({
                width: iW + 'px', height: iH + 'px'
            });

            if (r.html) {
                $('#lhgdig_inbox', dig).css({
                    width: iW + 'px', height: iH + 'px'
                });
            }

            this.SetLoadLeft();
        };

        this.reDialogSize = function(width, height) {
            $(this.dlg).css({
                'width': width + 'px', 'height': height + 'px'
            });

            this.reContentSize(this.dlg);
        };

        this.initDrag = function(elem) {
            var lacoor, maxX, maxY, curpos, regw = this.regWindow, cS, sS;

            function moveHandler(ev) {
                var curcoor = { x: ev.screenX, y: ev.screenY };
                curpos =
		    {
		        x: curpos.x + (curcoor.x - lacoor.x),
		        y: curpos.y + (curcoor.y - lacoor.y)
		    };
                lacoor = curcoor;

                if (r.rang) {
                    if (curpos.x < sS.x) curpos.x = sS.x;
                    if (curpos.y < sS.y) curpos.y = sS.y;
                    if (curpos.x > maxX) curpos.x = maxX;
                    if (curpos.y > maxY) curpos.y = maxY;
                }

                $(dragDiv).css({ left: curpos.x + 'px', top: curpos.y + 'px' });
            };

            function upHandler(ev) {
                for (var i = 0, l = regw.length; i < l; i++) {
                    $(regw[i].document).unbind('mousemove', moveHandler);
                    $(regw[i].document).unbind('mouseup', upHandler);
                }

                if ($.browser.msie)
                    dragDiv.releaseCapture();

                dragDiv.style.display = 'none'; lacoor = null; elem = null;

                if (self.opt.fixed)
                    $(self.dlg).css({ left: curpos.x - sS.x + 'px', top: curpos.y - sS.y + 'px' });
                else
                    $(self.dlg).css({ left: curpos.x + 'px', top: curpos.y + 'px' });
            };

            $(elem).bind('mousedown', function(ev) {
                if (ev.target.id === 'lhgdig_xbtn') return;

                cS = $.ui.getClientSize(top);
                sS = $.ui.getScrollSize(top);

                var lt = self.dlg.offsetLeft,
			    tp = self.dlg.offsetTop,
			    dW = self.dlg.clientWidth,
			    dH = self.dlg.clientHeight;

                curpos = self.opt.fixed ?
			    { x: lt + sS.x, y: tp + sS.y} : { x: lt, y: tp };

                lacoor = { x: ev.screenX, y: ev.screenY };

                maxX = self.opt.fixed ? cS.w - dW : cS.w + sS.x - dW;
                maxY = self.opt.fixed ? cS.h - dH : cS.h + sS.y - dH;

                $(dragDiv).css({
                    width: dW - 2 + 'px', height: dH - 2 + 'px', left: curpos.x + 'px',
                    top: curpos.y + 'px', zIndex: parseInt(ZIndex, 10) + 2, display: ''
                });

                for (var i = 0, l = regw.length; i < l; i++) {
                    $(regw[i].document).bind('mousemove', moveHandler);
                    $(regw[i].document).bind('mouseup', upHandler);
                }

                ev.preventDefault();

                if ($.browser.msie) dragDiv.setCapture();
            });
        };

        this.initSize = function(elem) {
            var lacoor, dH, dW, curpos, regw = this.regWindow, dialog, cS, sS;

            function moveHandler(ev) {
                var curcoor = { x: ev.screenX, y: ev.screenY };
                dialog = {
                    w: curcoor.x - lacoor.x,
                    h: curcoor.y - lacoor.y
                };

                if (dialog.w < 200) dialog.w = 200;
                if (dialog.h < 100) dialog.h = 100;

                $(dragDiv).css({
                    width: dialog.w + 'px', height: dialog.h + 'px',
                    top: curpos.y + 'px', left: curpos.x + 'px'
                });
            };

            function upHandler(ev) {
                for (var i = 0, l = regw.length; i < l; i++) {
                    $(regw[i].document).unbind('mousemove', moveHandler);
                    $(regw[i].document).unbind('mouseup', upHandler);
                }

                if ($.browser.msie)
                    dragDiv.releaseCapture();
                self.reDialogSize(dialog.w, dialog.h);
                dragDiv.style.display = 'none'; lacoor = null; elem = null;
            };

            $(elem).bind('mousedown', function(ev) {
                dW = self.dlg.clientWidth;
                dH = self.dlg.clientHeight;

                dialog = { w: dW, h: dH };

                cS = $.ui.getClientSize(top);
                sS = $.ui.getScrollSize(top);

                var lt = self.dlg.offsetLeft,
			    tp = self.dlg.offsetTop;

                curpos = self.opt.fixed ?
			    { x: lt + sS.x, y: tp + sS.y} : { x: lt, y: tp };

                lacoor = { x: ev.screenX - dW, y: ev.screenY - dH };

                $(dragDiv).css({
                    width: dW - 2 + 'px', height: dH - 2 + 'px', left: curpos.x + 'px',
                    top: curpos.y + 'px', zIndex: parseInt(ZIndex, 10) + 2, display: ''
                });

                for (var i = 0, l = regw.length; i < l; i++) {
                    $(regw[i].document).bind('mousemove', moveHandler);
                    $(regw[i].document).bind('mouseup', upHandler);
                }

                ev.preventDefault();

                if ($.browser.msie) dragDiv.setCapture();
            });
        };

        this.setIndex = function(ev) {
            if (self.opt.fixed && $.browser.msie && !($.browser.version == "7.0")) {
                $(self.dlg).parent()[0].style.zIndex = parseInt(ZIndex, 10) + 1;
                ZIndex = parseInt($(self.dlg).parent()[0].style.zIndex, 10);
            }
            else {
                self.dlg.style.zIndex = parseInt(ZIndex, 10) + 1;
                ZIndex = parseInt(self.dlg.style.zIndex, 10);
            }

            ev.stopPropagation();
        };

        this.SetLoadLeft = function() {
            var loadL = ($('#lhgdig_content', this.dlg)[0].offsetWidth -
		    $('#lhgdig_load', this.dlg)[0].offsetWidth) / 2;
            $('#lhgdig_load', this.dlg)[0].style.left = loadL + 'px';
        };

        this.addBtn = function(id, txt, fn) {
            if ($('#' + id, this.dlg)[0])
                $('#' + id, this.dlg).html('<em>' + txt + '</em>').click(fn);
            else {
                var html = '<a id="' + id + '" class="lhgdig_button" href="javascript:void(0)" hidefocus="true"><em>' + txt + '</em></a>',

		    btn = $(html, doc).click(fn)[0];
                $('#lhgdig_bDiv', this.dlg).append(btn);
            }
        };

        this.removeBtn = function(id) {
            if ($('#' + id, this.dlg)[0])
                $('#' + id, this.dlg).remove();
        };

        this.reload = function(win, url) {
            win = win || window;
            self.cancel();
        };

        this.ShowCover = function() {
            if (!cover) {
                var html = ['<div style="position:absolute;top:0px;left:0px;',
					'background-color:#fff;">', iframe, '</div>'].join('');

                cover = $(html, doc).css('opacity', 0.5).appendTo(doc.body)[0];
            }

            $(top).bind('resize', reSizeHdl);
            reSizeHdl();
            $(cover).css({ display: '', zIndex: getZIndex() });
        };

        this.cancel = function() {
            var frm = $('#lhgfrm', self.dlg)[0];
            if (frm) {
                if (!self.opt.link)
                    $(frm.contentWindow).unbind('load');
                frm.src = getsrc(); frm = null;
            }

            self.regWindow = [];

            if (self.opt.fixed && $.browser.msie && !($.browser.version == "7.0")) {
                $('html', doc).removeClass('lhgdig_ie6_fixed');
                $(self.dlg).parent().remove();
            }
            else
                $(self.dlg).remove(); self.dlg = null;

            if (cover) {
                if (self.opt.parent && self.opt.parent.opt.cover) {
                    var Index = self.opt.parent.dlg.style.zIndex;
                    cover.style.zIndex = parseInt(Index, 10) - 1;
                }
                else
                    cover.style.display = 'none';
            }
        };

        this.cleanDialog = function() {
            if (self.dlg) {
                var frm = $('#lhgfrm', self.dlg)[0];
                if (frm) {
                    if (!self.opt.link)
                        $(frm.contentWindow).unbind('load');
                    frm.src = getsrc(); frm = null;
                }

                if (self.opt.fixed && $.browser.ie && !($.browser.version == "7.0")) {
                    $('html', doc).removeClass('lhgdig_ie6_fixed');
                    $(self.dlg).parent().remove();
                }
                else
                    $(self.dlg).remove(); self.dlg = null;
            }
        };

        $(window).bind('unload', this.cleanDialog);

        if (elem)
            $(elem).bind(r.event, function() { self.ShowDialog(); });
    };

    $(window).bind('unload', cleanDiv);

})(jQuery);