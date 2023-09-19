<style>
	.link-card {
		min-width: 60px;
		max-width: 100px;
		width: 33%;
		border: 1px solid #7f7f7f;
		margin: 3px;
		border-radius: 10px;
		background-color: aliceblue;
		padding: 5px;
		cursor: pointer;
	}
	.link-card:hover {
		background-color: #fffef3;
	}
	.link-card .image {
		border-bottom: 1px solid #9d9d9d;
		height: 100px;
		margin: auto;
	}
	.link-card img {
		width: 100%;
		display: block;
	}
</style>

<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../../nlp-api';
	export let link: UserLinkDto;

	const handleClick = () => {
		new UserLinksClient().recordLinkFollow(link.linkID).then((success: boolean) => {
			if (success) link.followCount += 1;
		});
		window.open(link.linkUrl, '_blank');
	};
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="link-card" on:click={handleClick}>
	<div class="image">
		<img src={`api/images/link/${link.linkImage}`} alt={link.linkName} />
	</div>
	{link.linkName}
	<div class="card-actions justify-end">
		<span class="id"># {link.linkID}</span>
		<span class="follow-count">{link.followCount}</span>
		<span class="order">{link.linkOrder}</span>
	</div>
</div>
