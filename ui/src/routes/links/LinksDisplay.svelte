<script lang="ts">
	import type { UserLinkDto } from '../../nlp-api';
	export let links: UserLinkDto[] = [];
	export let onLinkSelected: (link: UserLinkDto) => void;
</script>

<div class="links mt-2">
	{#each links as link}
		<!-- svelte-ignore a11y-click-events-have-key-events -->
		<div class="link shadow" on:click={() => onLinkSelected(link)}>
			<div class="image">
				<img src={`/api/image/user-link/${link.linkId}`} alt={link.linkName} />
				<span class="follow">{link.followCount}</span>
			</div>
			<p class="text-center text-truncate my-1">{link.linkName}</p>
		</div>
	{/each}
</div>

<style>
	.links {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}
	.link {
		border: 1px solid #6b6b6b;
		margin: 0.3em;
		border-radius: 1em;
		width: 120px;
		cursor: pointer;
	}
	.link:hover {
		opacity: 0.8;
	}
	.link .image {
		border-top-right-radius: 1em;
		border-top-left-radius: 1em;
		height: 120px;
		border-bottom: 1px solid #474747;
		background-color: #fff;
		position: relative;
	}
	.link img {
		width: 100%;
		border-top-right-radius: 1em;
		border-top-left-radius: 1em;
	}
	.follow {
		position: absolute;
		top: 0.4em;
		right: 0.4em;
		color: #00ff39;
		background-color: black;
		padding: 0.2em 0.4em;
		border-radius: 100px;
		opacity: 0.7;
		font-size: 0.8em;
		width: 30%;
		text-align: center;
		font-weight: 600;
	}
</style>
