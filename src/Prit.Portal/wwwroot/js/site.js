//classes
product = function (id, name, description, price) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.price = price;
}

//contexts
productContext = {
    bindEvents: function () {
        $("#btn-product-save").on("click", function () {
            var prod = new product($("#product-id").val(), $("#product-name").val(), $("#product-description").val(), $("#product-price").val())

            if (productContext.validateModel(prod)) {
                productContext.save(prod);
            }
        });

        $("#btn-product-delete").on("click", function () {
            console.log("Excluir: " + $("#delete-product-id").val());
            productContext.delete($("#delete-product-id").val());
        });

        $('#product-modal').on('show.bs.modal', function (event) {

            var button = $(event.relatedTarget);
            var modal = $(this)

            var id = button.data("id");
    
            if (id > 0) {
                $("span[data-valmsg-for='product-name']").text("");
                $("span[data-valmsg-for='product-description']").text("");
                $("span[data-valmsg-for='product-price']").text("");

                modal.find('#product-id').val(id);
                modal.find('#product-name').val($("td[data-id='" + id + "'][data-property='Name']").text());
                modal.find('#product-description').val($("td[data-id='" + id + "'][data-property='Description']").text());
                modal.find('#product-price').val($("td[data-id='" + id + "'][data-property='Price']").data("value"));
            }

            modal.show();
        });

        $('#delete-product-modal').on('show.bs.modal', function (event) {
            $("#product-id-confirm-delete").val("");
            $("#btn-product-delete").attr("disabled", true);

            var button = $(event.relatedTarget);
            var modal = $(this)

            var id = button.data("id");
            $("#delete-product-id").val(id);
            $("#delete-message").empty();
            $("#delete-message").append("<p>Para apagar o produto, digite exatamente o código <strong>" + id + "</strong> abaixo</p>")
            $("#product-id-confirm-delete").text("");

            modal.show();
        });

        $("#product-id-confirm-delete").keyup(function () {
            console.log($("#product-id-confirm-delete").val());
            console.log($("delete-product-id").val());
            $("#btn-product-delete").attr("disabled", !($("#product-id-confirm-delete").val() === $("#delete-product-id").val()));
        });
    },
    clearValidationData: function () {
        $("span[data-valmsg-for='product-name']").text("");
        $("span[data-valmsg-for='product-description']").text("");
        $("span[data-valmsg-for='product-price']").text("");
    },
    validateModel: function (product) {
        var result = true;

        $("span[data-valmsg-for='product-name']").text("");
        $("span[data-valmsg-for='product-description']").text("");
        $("span[data-valmsg-for='product-price']").text("");

        if (!product.name || product.name.length === 0 || !product.name.trim()) {
            $("span[data-valmsg-for='product-name']").text("Nome inválido");
            result = false;
        }

        if (!product.description || product.description.length === 0 || !product.description.trim()) {
            $("span[data-valmsg-for='product-description']").text("Descrição inválida");
            result = false;
        }

        if (!product.price || product.price.length === 0 || !product.price.trim() || product.price == 0) {
            $("span[data-valmsg-for='product-price']").text("Preço inválido");
            result = false;
        }

        return result;

    },
    enableDisableModalBtn: function (disabled) {
        $("#btn-product-save").attr("disabled", disabled);
        $("#btn-product-close").attr("disabled", disabled);
    },
    save: function (product) {

        productContext.enableDisableModalBtn(true);

        axios({
            method: 'post',
            url: '/product/upsert',
            data: product
        })
            .then(function (response) {
                location.reload();
            })
            .catch(function (error) {
                console.log(error);
            })
            .then(function () {
                productContext.enableDisableModalBtn(false);
            });
    },
    delete: function (id) {
        $("#btn-product-delete").attr("disabled", true);
        axios({
            method: 'delete',
            url: '/product/delete',
            params: {
                id: id
            }
        })
        .then(function (response) {
            location.reload();
        })
        .catch(function (error) {
            console.log(error);
        })
        .then(function () {
            $("#btn-product-delete").attr("disabled", false);
        });
    }
}

productContext.bindEvents();