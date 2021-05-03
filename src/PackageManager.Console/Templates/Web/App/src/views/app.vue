<template>
    <div>{{ message }}</div>
</template>
<script type="text/javascript">
    import Vue from 'vue';

    Vue.extend({
        data: function () {
            return {
                message: "Hello World"
            }
        }
    });
</script>