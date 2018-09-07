var selectApiClassificationID;
var setting = {
    view: {
        addHoverDom: null,
        removeHoverDom: null,
        selectedMulti: false
    },
    check: {
        enable: false
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    edit: {
        enable: false
    },
    callback: {
        onClick: itemClick
    }
};

function getApiClassificationsBySystem(systemNode, nodes) {
    $.ajax({
        type: 'GET',
        url: "/AdminAPIClassification/GetAPIClassifications2Json?system=" + systemNode.name,
        dataType: 'JSON',
        async: false,
        success: function (result) {
            var r = result;
            for (var i = 0; i < result.length; i++) {
                var classificationNode = { id: result[i].ID, pId: systemNode.id, name: result[i].Name };
                nodes.push(classificationNode);
            }
        }
    });
}

$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: "/AdminCustomerClass/GetSystems",
        dataType: 'text',
        async: false,
        success: function (result) {
            var nodes = [];
            var systems = result.split(",");
            for (var i = 0; i < systems.length; i++) {
                var systemNode = { id: -(i + 1), pId: 0, name: systems[i], isParent: true };
                nodes.push(systemNode);
                getApiClassificationsBySystem(systemNode, nodes);
            }
            $.fn.zTree.init($("#zTree"), setting, nodes);
        }
    })
});

function itemClick(event, treeId, treeNode, clickFlag) {
    doClick(event, treeId, treeNode, clickFlag);
};