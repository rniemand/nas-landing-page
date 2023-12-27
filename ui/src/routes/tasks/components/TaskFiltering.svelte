<script lang="ts">
	import TaskCategorySelect from './TaskCategorySelect.svelte';
	import TaskSubCategorySelect from './TaskSubCategorySelect.svelte';

	export let category: string | undefined = undefined;
	export let subCategory: string | undefined = undefined;
	export let allowAllOption: boolean = false;

	const categoryChanged = (_cat: string | undefined) => {
		subCategory = undefined;
	};

	$: categoryChanged(category);
</script>

<div class="w-100 d-flex">
	<TaskCategorySelect bind:value={category} {allowAllOption} />
	<TaskSubCategorySelect {category} bind:value={subCategory} {allowAllOption} />
</div>
