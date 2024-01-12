<script lang="ts">
	export let small: boolean = false;
	export let icon: string;
	export let name: string;
	export let url: string;
</script>

<a class="item" href={url} class:small>
	<i class="bi {icon}" />
	{name}
</a>

<style>
	.item {
		border: 1px solid #6f6f6f;
		width: 8em;
		display: block;
		padding: 0.5em 0.1em;
		border-radius: 0.5em;
		text-align: center;
		cursor: pointer;
		font-size: 1.2em;
		background-color: #292e32;
		margin: 0.2em;
		text-decoration: none;
		color: #fff;
	}
	.item.small {
		font-size: 0.8em;
	}
	.item:hover {
		background-color: #373e44;
	}
	.item i.bi {
		display: block;
		font-size: 3em;
	}
</style>
