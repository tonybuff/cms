webpackJsonp([1],{"/5n/":function(t,s){},0:function(t,s){},NHnr:function(t,s,e){"use strict";Object.defineProperty(s,"__esModule",{value:!0});var a=e("7+uW"),n=e("woOf"),i=e.n(n),o={props:{size:{type:Number},score:{type:Number}},data:function(){return{}},computed:{starType:function(){return"star-"+this.size},itemClasses:function(){for(var t=[],s=Math.floor(2*this.score)/2,e=s%1!=0,a=Math.floor(s),n=0;n<a;n++)t.push("on");for(e&&t.push("half");t.length<length;)t.push("off");return t}},methods:{}},r={render:function(){var t=this.$createElement,s=this._self._c||t;return s("div",{staticClass:"star",class:this.starType},this._l(this.itemClasses,function(t,e){return s("span",{staticClass:"star-item",class:t})}))},staticRenderFns:[]};var c=e("VU/8")(o,r,!1,function(t){e("/5n/")},"data-v-5fc886fc",null).exports,l={props:{seller:{type:Object}},data:function(){return{detailShow:!1}},created:function(){this.classMap=["descrease","discount","special","invoice","guarantee"]},methods:{showDetail:function(){this.detailShow=!0},hideDetail:function(){this.detailShow=!1}},components:{star:c}},d={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",{staticClass:"header"},[e("div",{staticClass:"content-wrapper"},[e("div",{staticClass:"avatar"},[e("img",{attrs:{src:t.seller.avatar,width:"64",height:"64"}})]),t._v(" "),e("div",{staticClass:"content"},[e("div",{staticClass:"title"},[e("span",{staticClass:"brand"}),t._v(" "),e("span",{staticClass:"name"},[t._v(t._s(t.seller.name))])]),t._v(" "),e("div",{staticClass:"description"},[t._v("\n\t\t\t\t"+t._s(t.seller.description)+"/"+t._s(t.seller.deliveryTime)+"分钟送达\n\t\t\t")]),t._v(" "),t.seller.supports?e("div",{staticClass:"support"},[e("span",{staticClass:"icon",class:t.classMap[t.seller.supports[0].type]}),t._v(" "),e("span",{staticClass:"text"},[t._v(t._s(t.seller.supports[0].description))])]):t._e()]),t._v(" "),t.seller.supports?e("div",{staticClass:"support-content",on:{click:t.showDetail}},[e("span",{staticClass:"count"},[t._v(" "+t._s(t.seller.supports.length)+"个 ")]),t._v(" "),e("i",{staticClass:"icon-keyboard_arrow_right"})]):t._e()]),t._v(" "),e("div",{staticClass:"bulletin-wrapper",on:{click:t.showDetail}},[e("span",{staticClass:"bulletin-title"}),t._v(" "),e("span",{staticClass:"bulletin-text"},[t._v(" "+t._s(t.seller.bulletin)+" ")]),t._v(" "),e("i",{staticClass:"icon-keyboard_arrow_right"})]),t._v(" "),e("div",{staticClass:"background"},[e("img",{attrs:{src:t.seller.avatar,width:"100%",height:"100%"}})]),t._v(" "),e("transition",{attrs:{name:"slide-fade",mode:"out-in"}},[e("div",{directives:[{name:"show",rawName:"v-show",value:t.detailShow,expression:"detailShow"}],staticClass:"detail"},[e("div",{staticClass:"detail-wrapper clearfix"},[e("div",{staticClass:"detail-main"},[e("h1",{staticClass:"name"},[t._v(" "+t._s(t.seller.name)+" ")]),t._v(" "),e("div",{staticClass:"star-wrapper"},[e("star",{attrs:{size:48,score:t.seller.score}})],1),t._v(" "),e("div",{staticClass:"title"},[e("div",{staticClass:"line"}),t._v(" "),e("div",{staticClass:"text"},[t._v("优惠信息")]),t._v(" "),e("div",{staticClass:"line"})]),t._v(" "),t.seller.supports?e("ul",{staticClass:"supports"},t._l(t.seller.supports,function(s,a){return e("li",{staticClass:"support-item"},[e("span",{staticClass:"icon",class:t.classMap[t.seller.supports[a].type]}),t._v(" "),e("span",{staticClass:"text"},[t._v(t._s(t.seller.supports[a].description))])])})):t._e(),t._v(" "),e("div",{staticClass:"title"},[e("div",{staticClass:"line"}),t._v(" "),e("div",{staticClass:"text"},[t._v("商家公共")]),t._v(" "),e("div",{staticClass:"line"})]),t._v(" "),e("div",{staticClass:"bulletin"},[e("p",{staticClass:"content"},[t._v("\n\t\t\t\t\t\t\t"+t._s(t.seller.bulletin)+"\n\t\t\t\t\t\t")])])])]),t._v(" "),e("div",{staticClass:"detail-close",on:{click:t.hideDetail}},[e("i",{staticClass:"icon-close"})])])])],1)},staticRenderFns:[]};var u={name:"app",components:{"v-header":e("VU/8")(l,d,!1,function(t){e("TQg/")},"data-v-fb2ef850",null).exports},data:function(){return{seller:{id:(t={},(s=window.location.search.match(/[?&][^?&]+=[^?&]+/g))&&s.forEach(function(s){var e=s.substring(1).split("="),a=decodeURIComponent(e[0]),n=decodeURIComponent(e[1]);t[a]=n}),t).id}};var t,s},created:function(){var t=this;console.log("组件被创建完成"),this.$http.get("/api/values/get?elmentName=seller").then(function(s){s=s.body,t.seller=i()({},t.seller,s),console.log(t.seller)})}},v={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",{attrs:{id:"app"}},[e("v-header",{attrs:{seller:t.seller}}),t._v(" "),e("div",{staticClass:"app-content"},[e("router-view")],1),t._v(" "),e("div",{staticClass:"tab"},[e("div",{staticClass:"tab-item"},[e("router-link",{attrs:{to:"/goods"}},[t._v("商品")])],1),t._v(" "),e("div",{staticClass:"tab-item"},[e("router-link",{attrs:{to:"/ratings"}},[t._v("评价")])],1),t._v(" "),e("div",{staticClass:"tab-item"},[e("router-link",{attrs:{to:"/seller"}},[t._v("商家")])],1)])],1)},staticRenderFns:[]};var p=e("VU/8")(u,v,!1,function(t){e("eyKU")},"data-v-726a8124",null).exports,f=e("/ocq"),_=e("8+8L"),h=new(0,e("7+uW").default),C={props:{food:{type:Object}},data:function(){return{}},methods:{addCart:function(t){t._constructed&&(this.food.count?this.food.count++:a.default.set(this.food,"count",1),h.$emit("cart.add",t.target))},descreaseCart:function(t){t._constructed&&this.food.count&&this.food.count--}}},m={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",{staticClass:"cartcontrol"},[e("transition",{attrs:{name:"move"}},[e("div",{directives:[{name:"show",rawName:"v-show",value:t.food.count>0,expression:"food.count > 0"}],staticClass:"cart-descrease",on:{click:function(s){return s.stopPropagation(),s.preventDefault(),t.descreaseCart(s)}}},[e("span",{staticClass:"inner icon-remove_circle_outline"})])]),t._v(" "),e("div",{directives:[{name:"show",rawName:"v-show",value:t.food.count>0,expression:"food.count >0"}],staticClass:"cart-count"},[t._v("\n        "+t._s(t.food.count)+"\n    ")]),t._v(" "),e("div",{staticClass:"cart-add icon-add_circle",on:{click:function(s){return s.stopPropagation(),s.preventDefault(),t.addCart(s)}}})],1)},staticRenderFns:[]};var w=e("VU/8")(C,m,!1,function(t){e("bj0M")},"data-v-2e5e6522",null).exports,g=e("43Vb"),b=e.n(g),x={props:{food:{type:Object}}},y={render:function(){var t=this.$createElement;return(this._self._c||t)("div",{directives:[{name:"show",rawName:"v-show",value:this.showFlag,expression:"showFlag"}],staticClass:"food"})},staticRenderFns:[]};var k=e("VU/8")(x,y,!1,function(t){e("r2Xf")},"data-v-4d078929",null).exports,$={props:{seller:{type:Object}},data:function(){return{goods:[],scrollY:0,selectedFood:{}}},created:function(){var t=this;this.classMap=["descrease","discount","special","invoice","guarantee"],this.$http.get("/api/values/get?elmentName=goods").then(function(s){s=s.body,t.goods=s,console.log(t.goods),t.$nextTick(function(){t._initScroll()}),s.errno})},methods:{_initScroll:function(){var t=this;this.menuScroll=new b.a(this.$refs.menuWrapper,{click:!0}),this.foodScroll=new b.a(this.$refs.foodWrapper,{click:!0,probeType:3}),this.foodScroll.on("scroll",function(s){t.scrollY=Math.abs(Math.round(s.y))})},selectFood:function(t,s){this.selectedFood=t}},components:{cartcontrol:w,food:k}},F={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",{staticClass:"goods"},[e("div",{ref:"menuWrapper",staticClass:"menu-wrapper"},[e("ul",t._l(t.goods,function(s){return e("li",{staticClass:"menu-item",on:{click:function(s){t.selectFood(t.food,s)}}},[e("span",{staticClass:"text"},[e("span",{directives:[{name:"show",rawName:"v-show",value:s.type>0,expression:"item.type >0"}],staticClass:"icon",class:t.classMap[s.type]}),t._v("\n\t\t\t\t\t"+t._s(s.name)+"\n\t\t\t\t")])])}))]),t._v(" "),e("div",{ref:"foodWrapper",staticClass:"foods-wrapper"},[e("ul",t._l(t.goods,function(s){return e("li",{staticClass:"food-list food-list-hook"},[e("h1",{staticClass:"title"},[t._v(t._s(s.name))]),t._v(" "),e("ul",t._l(s.foods,function(s,a){return e("li",{staticClass:"food-item border-1px"},[e("div",{staticClass:"icon"},[e("img",{staticClass:"img",attrs:{src:s.icon,width:"57",height:"57"}})]),t._v(" "),e("div",{staticClass:"content"},[e("h2",{staticClass:"name"},[t._v(t._s(s.name)+" ")]),t._v(" "),e("p",{staticClass:"desc"},[t._v(t._s(s.description))]),t._v(" "),e("div",{staticClass:"extra"},[e("span",{staticClass:"count"},[t._v("\n\t\t\t\t\t\t\t\t\t 月售"+t._s(s.sellCount)+"份\n\t\t\t\t\t\t\t\t ")])]),t._v(" "),e("div",{staticClass:"price"},[e("span",{staticClass:"now"},[t._v("\n\t\t\t\t\t\t\t\t\t ￥"+t._s(s.price)+"\n\t\t\t\t\t\t\t\t ")]),t._v(" "),e("span",{directives:[{name:"show",rawName:"v-show",value:s.oldPrice,expression:"food.oldPrice"}],staticClass:"old"},[t._v("\n\t\t\t\t\t\t\t\t\t ￥"+t._s(s.oldPrice)+"\n\t\t\t\t\t\t\t\t ")])]),t._v(" "),e("div",{staticClass:"cartcontrol-wrapper"},[e("cartcontrol",{attrs:{food:s}})],1)])])}))])}))]),t._v(" "),e("food",{attrs:{food:t.selectedFood}})],1)},staticRenderFns:[]};var M=e("VU/8")($,F,!1,function(t){e("x0yv")},"data-v-30e19a1b",null).exports,N=e("aYpo"),U={name:"ratings",data:function(){return{content:""}},components:{VueEditor:N.VueEditor},methods:{save:function(){this.$http.post("/api/auth",{content:this.content}).then(function(t){console.log(t.body)})}}},E={render:function(){var t=this,s=t.$createElement,e=t._self._c||s;return e("div",{staticClass:"ratings"},[t._v("\n\t\t评价\n\t\t"),e("button",{staticClass:"btn-send",on:{click:t.save}},[t._v("发送")]),t._v(" "),e("vue-editor",{attrs:{id:"editor"},model:{value:t.content,callback:function(s){t.content=s},expression:"content"}})],1)},staticRenderFns:[]};var V=e("VU/8")(U,E,!1,function(t){e("a+M/")},"data-v-2f293925",null).exports,R={render:function(){var t=this.$createElement;return(this._self._c||t)("div",[this._v("\n\t商家\n")])},staticRenderFns:[]},S=e("VU/8")({name:"seller"},R,!1,null,null,null).exports;e("zO3H");a.default.config.productionTip=!1,a.default.use(f.a),a.default.use(_.a);var O=[{path:"/goods",component:M},{path:"/ratings",component:V},{path:"/seller",component:S}],T=new f.a({linkActiveClass:"active",routes:O});new a.default({render:function(t){return t(p)},router:T}).$mount("#app");T.push("/goods")},"TQg/":function(t,s){},"a+M/":function(t,s){},bj0M:function(t,s){},eyKU:function(t,s){},r2Xf:function(t,s){},x0yv:function(t,s){},zO3H:function(t,s){}},["NHnr"]);
//# sourceMappingURL=app.29b64df2a9e517e65fe6.js.map