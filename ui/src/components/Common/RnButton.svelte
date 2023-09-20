<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	export let style: 'warning' = 'warning';
	export let outline: boolean = false;
	export let disabled: boolean = false;
	let classes: string = '';
	const dispatch = createEventDispatcher();

	const dispatchOnClick = (data: any) => dispatch('click', data);

	const generateClasses = () => {
		const generated: string[] = ['btn'];
		generated.push(`btn${outline ? '-outline' : ''}-${style}`);
		return generated.join(' ');
	};

	$: classes = generateClasses();
</script>

<button type="button" class={classes} {disabled} on:click={dispatchOnClick}>
	<slot />
</button>
